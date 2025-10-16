using System.ComponentModel.DataAnnotations;

namespace TodoListapp.Models.Dtos
{
    public class RegistrationUserRequestDto
    {
        [Required]

        public string Name { get; set; }

        [Required]

        public string Email { get; set; }

      
        [Required]


        public string Password { get; set; }

    }
}
