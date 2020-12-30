using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;
using TesteDotnet.Data;
using Microsoft.AspNetCore.Authorization;

namespace TesteDotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        public readonly IRepository _repo;

        public ProjectsController(IRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Projects
        [HttpGet]
        public ActionResult<IEnumerable<Project>> GetProject()
        {
            return _repo.GetAllProjects();
        }

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
