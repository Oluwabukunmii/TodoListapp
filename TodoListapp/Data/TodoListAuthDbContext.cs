using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TodoListapp.Data
{
    public class TodoListAuthDbContext : IdentityDbContext

    {
        public TodoListAuthDbContext(DbContextOptions<TodoListAuthDbContext> options) : base (options)
        {
            
        }
    }
}
