using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Request
{
    public class AddDeveloperToProjectRequestDTO : IRequestDTO
    {
        [Required(ErrorMessage = "Informe um email válido")]
        public Guid DeveloperId { get; set; }
    }
}
