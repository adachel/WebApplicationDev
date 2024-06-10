using System.ComponentModel.DataAnnotations;

namespace HW4.DTO
{
    public class UserDTO
    {
        [Required] // делает поле обязательным
        [EmailAddress]  // проверяет, что это email
        public string Email { get; set; }
        [Required]

        public string Password { get; set; }
        public UserRole UserRole { get; set; }
    }
}
