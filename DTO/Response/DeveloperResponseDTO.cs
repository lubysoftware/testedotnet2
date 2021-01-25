using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Response
{
    //CM_Area
    public class DeveloperResponseDTO : IResponseDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
