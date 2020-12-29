using System.Collections.Generic;

namespace Api.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Developer> Developers { get; set; }

        public List<Entry> Entries { get; set; }
    }
}