using System;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Domain.Works.Dtos
{
    public class WorkProjectCreateDto
    {
        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public Guid DeveloperId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
        
        [Required]
        public DateTime EndTime { get; set; }
        
        [Required]
        [MaxLength(300)]
        public string Comment { get; set; }
    }
}
