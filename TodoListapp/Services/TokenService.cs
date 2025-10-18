using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TodoListapp.Interfaces;

namespace TodoListapp.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateJWTToken(IdentityUser user)
        {

            // CREATE CLAIMS

            var claims = new List<Claim>(); //I think identityUser class for authentication, uses info in the user domain or dto


            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())); // Convert Guid to string for token




            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])); //Reads secret key from appsetting.json, used to digitally sign the token proven its from your server
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);   //means same key will be used for both signing andverifying the token later

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],    //the entity that issued the token (your app or API).
                configuration["Jwt:Audience"],  //who the token is meant for (e.g., your frontend app).
                claims,                         //key-value pairs (like user id, role, email) inside the token payload.
                expires: DateTime.Now.AddMinutes(15),  //sets how long the token is valid (here, 15 minutes).
                signingCredentials: credentials);     //attaches the signature to the token for verification.

            return new JwtSecurityTokenHandler().WriteToken(token);




        }
    }
}
