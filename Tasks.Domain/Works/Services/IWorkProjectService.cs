using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasks.Domain._Common.Dtos;
using Tasks.Domain._Common.Results;
using Tasks.Domain.Works.Dtos;

namespace Tasks.Domain.Works.Services
{
    public interface IWorkProjectService
    {
        Task<Result<WorkProjectDetailDto>> GetProjectByIdAsync(Guid id);
        Task<IEnumerable<WorkProjectListDto>> ListProjectsAsync(PaginationDto paginationDto);
        Task<Result> CreateProjectAsync(WorkProjectCreateDto workDto);
        Task<Result> UpdateProjectAsync(WorkProjectUpdateDto workDto);
        Task<Result> DeleteProjectAsync(Guid projectId, Guid developerId);
    }
}
