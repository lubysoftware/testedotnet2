using System.ComponentModel.DataAnnotations;

namespace Tasks.Domain.Developers.Dtos
{
    public class LoginDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
