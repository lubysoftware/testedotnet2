using System;

namespace Tasks.Domain.Projects.Dtos
{
    public class ProjectDetailDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
