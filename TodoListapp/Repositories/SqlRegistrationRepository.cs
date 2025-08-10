using Microsoft.EntityFrameworkCore;
using TodoListapp.Data;
using TodoListapp.Models.Domain;

namespace TodoListapp.Repositories
{
    public class SqlRegistrationRepository : IRegistrationRepository
    {
        private readonly Todolistdbcontext dbContext;

        public SqlRegistrationRepository(Todolistdbcontext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Registration> CreateAsync(Registration Registration)
        {

            await dbContext.Registration.AddAsync(Registration);
            dbContext.SaveChanges();
            return Registration;
        }
    }
}
