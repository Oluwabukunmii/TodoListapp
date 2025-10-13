using Microsoft.AspNetCore.Antiforgery;
using TodoListapp.Enums;

namespace TodoListapp.Models.Domain
{
    public class TodoItem
    {
        public Guid Id { get; set; }


        public string  Title { get; set; }

        public string? Description { get; set; }


        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public  TodoItemStatus Status { get; set; } 


        //relationship

        public Guid UserId { get; set; } 


        public User User { get; set; }




      




    }
}
