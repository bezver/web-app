using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public class UserCredentials
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
