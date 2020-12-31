using System;

namespace TesteDotnet.V1.Dtos
{
    public class EntryDto
    {
        public int Id { get; set; }

        public DateTime InitialDate { get; set; }

        public DateTime EndDate { get; set; }

        public int DeveloperId { get; set; }

        public int ProjectId { get; set; }
    }
}
