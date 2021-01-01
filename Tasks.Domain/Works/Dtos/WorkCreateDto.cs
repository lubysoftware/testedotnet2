using System;

namespace Tasks.Domain.Works.Dtos
{
    public class WorkCreateDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid DeveloperId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Comment { get; set; }
        public int Hours { get; set; }
    }
}
