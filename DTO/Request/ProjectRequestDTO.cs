using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Request
{
    public class ProjectRequestDTO : IRequestDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
