using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Entry
    {
        public Entry()
        { }
        public Entry(int id, DateTime initialDate, DateTime endDate, int developerId, int projectId)
        {
            Id = id;
            InitialDate = initialDate;
            EndDate = endDate;
            DeveloperId = developerId;
            ProjectId = projectId;
        }
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime InitialDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int DeveloperId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public Developer Developer { get; set; }
 

        public Project Project { get; set; }
    }
}