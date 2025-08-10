using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoListapp.Models.Domain;
using TodoListapp.Models.Dtos;
using TodoListapp.Repositories;

namespace TodoListapp.Controllers
{
    //https://localhost:portnumber/api/Registration
    [Route("api/[controller]")]
    [ApiController]
    

    public class Registrationcontoller : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRegistrationRepository registrationRepository;

        public Registrationcontoller(IMapper mapper , IRegistrationRepository registrationRepository)
        {
            this.mapper = mapper;
            this.registrationRepository = registrationRepository;
        n
        //CREATE WALKS
        // POST : /api/walks
        [HttpPost]

        public async Task<IActionResult> Create([FromBody] AddRegistrationRequestDto AddRegistrationRequestDto)
        {
            //Map Dto to domain Model

            var registrationDomainModel = mapper.Map<Registration>(AddRegistrationRequestDto);

            await registrationRepository.CreateAsync(registrationDomainModel);

            //Map domain Model to dto

            return Ok(mapper.Map<RegistrationDto>(registrationDomainModel));

        }


    } 

}
