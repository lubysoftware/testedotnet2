using System;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Domain.Projects.Dtos
{
    public class ProjectCreateDto
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}
