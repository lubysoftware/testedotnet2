using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasks.Domain._Common.Dtos;
using Tasks.Domain._Common.Results;
using Tasks.Domain.Projects.Dtos;
using Tasks.Domain.Projects.Repositories;

namespace Tasks.Domain.Projects.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public Task<Result> CreateProjectAsync(ProjectCreateDto projectDto)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteProjectAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<ProjectDetailDto>> GetProjectByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProjectListDto>> ListProjectAsync(PaginationDto paginationDto)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateProjectAsync(ProjectUpdateDto projectDto)
        {
            throw new NotImplementedException();
        }
    }
}
