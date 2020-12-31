using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Domain._Common.Dtos;
using Tasks.Domain._Common.Enums;
using Tasks.Domain._Common.Results;
using Tasks.Domain.Projects.Dtos;
using Tasks.Domain.Projects.Entities;
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

        public async Task<Result> CreateProjectAsync(ProjectCreateDto projectDto)
        {
            var existTitle = await _projectRepository.ExistByTitle(projectDto.Title);
            if (existTitle) return new Result(Status.Conflict, $"Project with {nameof(projectDto.Title)} already exist");

            var project = new Project(
                id: projectDto.Id,
                title: projectDto.Title,
                description: projectDto.Description
            );

            await _projectRepository.CreateAsync(project);
            return new Result();
        }

        public async Task<Result> DeleteProjectAsync(Guid id)
        {
            var existProject = await _projectRepository.ExistAsync(id);
            if (!existProject) return new Result(Status.NotFund, $"Project with {nameof(id)} does not exist");

            var project = await _projectRepository.GetByIdAsync(id);
            await _projectRepository.DeleteAsync(project);
            return new Result();
        }

        public async Task<Result<ProjectDetailDto>> GetProjectByIdAsync(Guid id)
        {
            var existProject = await _projectRepository.ExistAsync(id);
            if (!existProject) return new Result<ProjectDetailDto>(Status.NotFund, $"Project with {nameof(id)} does not exist");

            var project = await _projectRepository.GetByIdAsync(id);
            var projectDetail = new ProjectDetailDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description
            };

            return new Result<ProjectDetailDto>(projectDetail);
        }

        public async Task<IEnumerable<ProjectListDto>> ListProjectsAsync(PaginationDto pagination)
        {
            var projectsList = _projectRepository.Query()
                .Skip(pagination.Offset)
                .Take(pagination.Limit)
                .Select(d => new ProjectListDto
                {
                    Id = d.Id,
                    Title = d.Title
                })
                .ToArray();

            return await Task.FromResult(projectsList);
        }

        public async Task<Result> UpdateProjectAsync(ProjectUpdateDto projectDto)
        {
            var existProject = await _projectRepository.ExistAsync(projectDto.Id);
            if (!existProject) return new Result(Status.NotFund, $"Project with {nameof(projectDto.Id)} does not exist");
            var existTitle = await _projectRepository.ExistByTitle(projectDto.Title, projectDto.Id);
            if (existTitle) return new Result(Status.Conflict, $"Project with {nameof(projectDto.Title)} already exist");

            var project = await _projectRepository.GetByIdAsync(projectDto.Id);
            project.SetData(
                title: projectDto.Title,
                description: projectDto.Description
            );

            await _projectRepository.UpdateAsync(project);
            return new Result();
        }
    }
}
