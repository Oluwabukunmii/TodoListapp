using TodoListapp.Enums;
using TodoListapp.Models.Domain;

namespace TodoListapp.Models.Dtos
{
    public class TodoItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

    


        public TodoItemStatus Status { get; set; }

    }
}
