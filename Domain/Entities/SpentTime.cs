using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class SpentTime : IIdentityEntity
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime StartedAt { get; set; }

        [Required]
        public DateTime FinishedAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public Guid DeveloperId { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        public Developer Developer { get; set; }

        public Project Project { get; set; }
    }
}
