using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Request
{
    public class AuthUserRequestDTO : IRequestDTO
    {
        [Required(ErrorMessage = "Informe um CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Informe o password")]
        public string Password { get; set; }
    }
}
