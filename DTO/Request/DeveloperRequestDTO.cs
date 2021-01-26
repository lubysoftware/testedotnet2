using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Request
{
    public class DeveloperRequestDTO : IRequestDTO
    {
        public string Name { get; set; }

        public string Cpf { get; set; }

        public string Password { get; set; }
    }
}
