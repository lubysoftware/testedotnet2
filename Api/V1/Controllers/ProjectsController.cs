using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;
using TesteDotnet.Data;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using TesteDotnet.V1.Dtos;
using System.Threading.Tasks;
using TesteDotnet.Helpers;

namespace TesteDotnet.V1.Controllers
{
    /// <summary>
    /// Controller for Projects
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProjectsController : ControllerBase
    {
        public readonly IRepository _repo;
        private IMapper _mapper { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public ProjectsController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all projects 
        /// </summary>
        /// <returns></returns>
        // GET: api/Projects
        // Pagination Example: v1/Projects?pageNumber=2&pageSize=10
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProject([FromQuery] PageParams pageParams)
        {
            var projects = await _repo.GetAllProjectsAsync(pageParams);
            var projectsResult = _mapper.Map<IEnumerable<ProjectDto>>(projects);

            Response.AddPagination(projects.CurrentPage, projects.PageSize, projects.TotalPages, projects.TotalCount);

            return Ok(projectsResult);
        }

        /// <summary>
        /// Get a project using an ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Projects/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _repo.GetProjectByIdAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        /// <summary>
        /// Update a project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectDto"></param>
        /// <returns></returns>
        // PUT: api/Projects/id
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, ProjectDto projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);
            if (id != project.Id)
            {
                return BadRequest();
            }

            _repo.Update(project);

            try
            {
                if (_repo.SaveChanges())
                {
                    return Ok(project);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                bool prjExists = await ProjectExists(id);
                if (!prjExists)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return BadRequest("Project not updated");
        }

        /// <summary>
        /// Create a project
        /// </summary>
        /// <param name="projectDto"></param>
        /// <returns></returns>
        // POST: api/Projects
        [Authorize]
        [HttpPost]
        public ActionResult<Project> PostProject(ProjectDto projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);
            _repo.Add(project);
            if (_repo.SaveChanges())
            {
                return Ok(project);
            }

            return BadRequest("Project not added");
        }

        /// <summary>
        /// Delete a Project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Projects/id
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _repo.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _repo.Delete(project);
            if (_repo.SaveChanges())
            {
                return Ok(project);
            }

            return BadRequest("Project not deleted");
        }

        private async Task<bool> ProjectExists(int id)
        {
            var project = await _repo.GetProjectByIdAsync(id);
            if (project != null)
            {
                return true;
            }
            return false;
        }
    }
}
