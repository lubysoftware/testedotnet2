using System;
using Tasks.Domain.Developers.Dtos;

namespace Tasks.Domain.Projects.Dtos
{
    public class ProjectWorkListDto
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DeveloperListDto Developer { get; set; }
        public string Comment { get; set; }
        public int Hours { get; set; }
    }
}
