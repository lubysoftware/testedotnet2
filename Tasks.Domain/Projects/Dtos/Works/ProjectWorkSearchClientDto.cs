using System;
using Tasks.Domain._Common.Dtos;

namespace Tasks.Domain.Projects.Dtos.Works
{
    public class ProjectWorkSearchClientDto : PaginationDto
    {
        public Guid? DeveloperId { get; set; }
    }
}
