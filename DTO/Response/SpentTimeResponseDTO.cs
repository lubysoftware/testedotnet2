using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Response
{
    public class SpentTimeResponseDTO : IResponseDTO
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
