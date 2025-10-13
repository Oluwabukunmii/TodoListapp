using AutoMapper;
using TodoListapp.Models.Domain;
using TodoListapp.Models.Dtos;

namespace TodoListapp.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegistrationUserRequestDto, User>().ReverseMap();
            CreateMap<User ,userDto >().ReverseMap();
            CreateMap<CreateTodoItemDto,TodoItem >().ReverseMap();
            CreateMap<TodoItem ,TodoItemDto >().ReverseMap();
           CreateMap<TodoItem,updateTodoDto >().ReverseMap();


        }


    }
}
