using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;
using TesteDotnet.Data;
using Microsoft.AspNetCore.Authorization;

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        public ProjectsController(IRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Get all projects 
        /// </summary>
        /// <returns></returns>
        // GET: api/Projects
        [HttpGet]
        public ActionResult<IEnumerable<Project>> GetProject()
        {
            return _repo.GetAllProjects();
        }

        /// <summary>
        /// Get a project using an ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Projects/id
        [HttpGet("{id}")]
        public ActionResult<Project> GetProject(int id)
        {
            var project = _repo.GetProjectById(id);

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
        /// <param name="project"></param>
        /// <returns></returns>
        // PUT: api/Projects/id
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult PutProject(int id, Project project)
        {
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
                if (!ProjectExists(id))
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
        /// <param name="project"></param>
        /// <returns></returns>
        // POST: api/Projects
        [Authorize]
        [HttpPost]
        public ActionResult<Project> PostProject(Project project)
        {
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
        public IActionResult DeleteProject(int id)
        {
            var project = _repo.GetProjectById(id);
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

        private bool ProjectExists(int id)
        {
            if (_repo.GetProjectById(id) != null)
            {
                return true;
            }
            return false;
        }
    }
}
