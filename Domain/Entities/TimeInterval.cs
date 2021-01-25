using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class TimeInterval : IIdentityEntity
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        
        [Required]
        public DateTime UpdatedAt { get; set; }

        public User User { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
