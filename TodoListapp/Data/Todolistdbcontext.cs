using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TodoListapp.Models.Domain;

namespace TodoListapp.Data
{
    public class Todolistdbcontext : DbContext
    {
        public Todolistdbcontext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

       

        public DbSet<Registration> Registration { get; set; }



    



       



     }

}
