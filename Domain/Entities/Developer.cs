using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Developer : IIdentityEntity
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        
        [Required]
        public DateTime UpdatedAt { get; set; }

        public ICollection<TimeInterval> TimeIntervals { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
