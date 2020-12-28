using System;

namespace Api.Models
{
    public class Entry
    {
        public int Id { get; set; }

        public DateTime InitialDate { get; set; }

        public DateTime EndDate { get; set; }

        public int DeveloperId { get; set; }

        public Developer Developer { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }
    }
}