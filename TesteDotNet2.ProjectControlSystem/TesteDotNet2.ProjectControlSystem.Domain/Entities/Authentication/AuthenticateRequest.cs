using System.ComponentModel.DataAnnotations;

namespace TesteDotNet2.ProjectControlSystem.Domain.Entities.Authentication
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
