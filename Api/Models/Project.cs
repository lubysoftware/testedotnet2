using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Project
    {
        public Project()
        { }
        public Project(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<DeveloperProject> DeveloperProject { get; set; }

        public IEnumerable<Entry> Entries { get; set; }
    }
}