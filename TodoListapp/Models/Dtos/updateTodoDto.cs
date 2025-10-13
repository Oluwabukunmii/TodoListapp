using TodoListapp.Enums;

namespace TodoListapp.Models.Dtos
{
    public class updateTodoDto
    {

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        //     public Guid UserId { get; set; }
        public TodoItemStatus Status { get; set; }

    }
}
