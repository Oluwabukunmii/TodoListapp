using AutoMapper;
using TodoListapp.Models.Domain;
using TodoListapp.Models.Dtos;

namespace TodoListapp.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AddRegistrationRequestDto, Registration>().ReverseMap();
            CreateMap<Registration ,RegistrationDto >().ReverseMap();


        }


    }
}
