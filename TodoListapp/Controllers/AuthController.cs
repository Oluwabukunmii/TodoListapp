using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListapp.CustomActionFilters;
using TodoListapp.Data;
using TodoListapp.Interfaces;
using TodoListapp.Models.Domain;
using TodoListapp.Models.Dtos;

namespace TodoListapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRespository;
        private readonly Todolistdbcontext todolistdbcontext;

        public AuthController(UserManager<IdentityUser> userManager , ITokenRepository tokenRespository, Todolistdbcontext todolistdbcontext)
        {
            this.userManager = userManager;
            this.tokenRespository = tokenRespository;
            this.todolistdbcontext = todolistdbcontext;
        }
        [Route("Register")]
        [HttpPost]
        [ValidateModel]

        public async Task<IActionResult> Register([FromBody] RegistrationUserRequestDto registrationUserRequestDto)
        {
            // Create Identity user
            var identityUser = new IdentityUser
            {
                UserName = registrationUserRequestDto.Name,
                Email = registrationUserRequestDto.Email
            };

            var identityResult = await userManager.CreateAsync(identityUser, registrationUserRequestDto.Password); 
            if (!identityResult.Succeeded)
            {
                return BadRequest("Registration failed");
            }

            //  Add matching record in your custom Users table
            var appUser = new User
            {
                Id = Guid.Parse(identityUser.Id), // convert string to Guid
                Name = registrationUserRequestDto.Name,
                Email = registrationUserRequestDto.Email,
            };

            todolistdbcontext.Users.Add(appUser);
            await todolistdbcontext.SaveChangesAsync();

           

           
            return Ok("User created successfully");
        }



        //POST : /api/Auth/Login

        [Route("Login")]
        [HttpPost]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)

        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Email); // Basically , its the information in the loginrequestdto thats manged in the identityuser used to manage user

            if (user != null)

            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.password);

                if (checkPasswordResult == true)
                {

                    // Create token

                    var jwtToken = tokenRespository.CreateJWTToken(user);

                    var response = new loginResponseDto

                    {
                        JwtToken = jwtToken

                    };



                    return Ok(response);

                }






            }

            

                return BadRequest("email or password is incorrect");

            
        }

    }

}