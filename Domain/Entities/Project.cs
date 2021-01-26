using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Project : IIdentityEntity
    {

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        
        [Required]
        public DateTime UpdatedAt { get; set; }

        public ICollection<SpentTime> SpentTimes { get; set; } = new List<SpentTime>();

        public ICollection<Developer> Developers { get; set; } = new List<Developer>();
    }
}
