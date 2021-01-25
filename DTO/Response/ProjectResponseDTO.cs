using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Response
{
    public class ProjectResponseDTO : IResponseDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
