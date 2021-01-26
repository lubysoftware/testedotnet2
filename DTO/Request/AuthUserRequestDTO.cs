using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Request
{
    public class AuthUserRequestDTO : IRequestDTO
    {        
        [EmailAddress(ErrorMessage="Informe um email válido")]
        [Required(ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o password")]
        public string Password { get; set; }
    }
}
