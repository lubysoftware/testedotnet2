using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Response
{
    public class DeveloperResponseDTO : IResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
