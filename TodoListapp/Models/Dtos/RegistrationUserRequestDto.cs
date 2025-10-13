using System.ComponentModel.DataAnnotations;

namespace TodoListapp.Models.Dtos
{
    public class RegistrationUserRequestDto
    {

        public string Name { get; set; }


        public string Email { get; set; }

      /*  [Required]

        [MinLength(8, ErrorMessage = "Password has to be a minimum length of 8 characters")]

        [MaxLength(20)]
      */


        public string Password { get; set; }

    }
}
