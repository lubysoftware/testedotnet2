using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasks.Domain._Common.Dtos;
using Tasks.Domain._Common.Results;
using Tasks.Domain.Projects.Dtos;
using Tasks.Domain.Projects.Services;

namespace Tasks.API.Controllers
{
    public class ProjectsController : TasksControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("{id}")]
        public async Task<Result<ProjectDetailDto>> GetProjectAsync([FromRoute] Guid id)
        {
            return GetResult(await _projectService.GetProjectByIdAsync(id));
        }

        [HttpGet]
        public async Task<Result<IEnumerable<ProjectListDto>>> ListProjectsAsync([FromQuery] PaginationDto paginationDto)
        {
            return GetResult(await _projectService.ListProjectsAsync(paginationDto));
        }

        [HttpPost]
        public async Task<Result> CreateProjectAsync([FromBody] ProjectCreateDto projectDto)
        {
            return GetResult(await _projectService.CreateProjectAsync(projectDto));
        }

        [HttpPut("{id}")]
        public async Task<Result> UpdateProjectAsync([FromBody] ProjectUpdateDto projectDto, [FromRoute] Guid id)
        {
            projectDto.Id = id;
            return GetResult(await _projectService.UpdateProjectAsync(projectDto));
        }

        [HttpDelete("{id}")]
        public async Task<Result> DeleteProjectAsync([FromRoute] Guid id) 
        { 
            return GetResult(await _projectService.DeleteProjectAsync(id));
        }
    }
}
