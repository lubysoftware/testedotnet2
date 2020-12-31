using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasks.Domain._Common.Dtos;
using Tasks.Domain._Common.Results;
using Tasks.Domain.Works.Dtos;
using Tasks.Domain.Works.Repositories;
using Tasks.Domain.Works.Services;

namespace Tasks.Services.Works
{
    public class WorkProjectService : IWorkProjectService
    {
        private readonly IWorkRepository _workRepository;

        public WorkProjectService(IWorkRepository workRepository)
        {
            _workRepository = workRepository;
        }

        public Task<Result> CreateProjectAsync(WorkProjectCreateDto workDto)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteProjectAsync(Guid projectId, Guid developerId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<WorkProjectDetailDto>> GetProjectByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WorkProjectListDto>> ListProjectsAsync(PaginationDto paginationDto)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateProjectAsync(WorkProjectUpdateDto workDto)
        {
            throw new NotImplementedException();
        }
    }
}
