using TodoListapp.Models.Domain;

namespace TodoListapp.Repositories
{
    public interface IRegistrationRepository
    {
       Task<Registration> CreateAsync(Registration Registration);


    }
}
