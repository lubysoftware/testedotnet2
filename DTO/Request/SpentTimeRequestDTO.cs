using Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Request
{
    public class SpentTimeRequestDTO : IRequestDTO
    {
        [Required]
        public DateTime StartedAt { get; set; }

        [Required]
        public DateTime FinishedAt { get; set; }
    }
}
