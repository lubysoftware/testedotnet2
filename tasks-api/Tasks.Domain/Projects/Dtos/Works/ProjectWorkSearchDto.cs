using System;
using Tasks.Domain._Common.Dtos;

namespace Tasks.Domain.Projects.Dtos.Works
{
    public class ProjectWorkSearchDto : PaginationDto
    {
        public Guid? DeveloperId { get; set; }
        public Guid ProjectId { get; set; }

        public ProjectWorkSearchDto(ProjectWorkSearchClientDto dto, Guid projectId)
        {
            Page = dto.Page;
            Limit = dto.Limit;
            DeveloperId = dto.DeveloperId;
            ProjectId = projectId;
        }
    }
}
