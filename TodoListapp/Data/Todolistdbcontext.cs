using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TodoListapp.Models.Domain;

namespace TodoListapp.Data
{
    public class Todolistdbcontext : DbContext
    {
        public Todolistdbcontext(DbContextOptions <Todolistdbcontext> dbContextOptions) : base(dbContextOptions)
        {
        }

       

        public DbSet<User> Users { get; set; }

        public DbSet<TodoItem> TodoItems { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define relationship
            modelBuilder.Entity<TodoItem>()
                .HasOne(t => t.User)
                .WithMany(u => u.TodoItems)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }





    }

}
