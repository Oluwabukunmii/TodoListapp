using System.ComponentModel.DataAnnotations;
using TodoListapp.Enums;

namespace TodoListapp.Models.Dtos
{
    public class CreateTodoItemDto
    {


        [Required]
        [MinLength(2, ErrorMessage = "Code has to be a minimum length of 1 characters")]
        [MaxLength(20, ErrorMessage = "Code has to be a maximum length of 20 characters")]

        public string Title { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum length of 100 characters")]
        public string ? Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

   //     public Guid UserId { get; set; }
        public TodoItemStatus Status { get; set; }

    }
}
