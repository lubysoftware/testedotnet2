using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;
using TesteDotnet.Data;
using Microsoft.AspNetCore.Authorization;

namespace TesteDotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        public readonly IRepository _repo;

        public DevelopersController(IRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Developers
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Developer>> GetDeveloper()
        {
            return _repo.GetAllDevelopers();
        }

        // GET: api/Developers/id
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Developer> GetDeveloper(int id)
        {
            var developer = _repo.GetDeveloperById(id);

            if (developer == null)
            {
                return NotFound();
            }

            return developer;
        }

        // PUT: api/Developers/id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult PutDeveloper(int id, Developer developer)
        {
            if (id != developer.Id)
            {
                return BadRequest();
            }

            _repo.Update(developer);

            try
            {
                if (_repo.SaveChanges())
                {
                    return Ok(developer);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeveloperExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return BadRequest("Developer not updated");
        }

        // POST: api/Developers
        [Authorize]
        [HttpPost]
        public ActionResult<Developer> PostDeveloper(Developer developer)
        {
            _repo.Add(developer);
            if (_repo.SaveChanges())
            {
                return Ok(developer);
            }

            return BadRequest("Developer not added");
        }

        // DELETE: api/Developers/id
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteDeveloper(int id)
        {
            var developer = _repo.GetDeveloperById(id);
            if (developer == null)
            {
                return NotFound();
            }

            _repo.Delete(developer);
            if (_repo.SaveChanges())
            {
                return Ok(developer);
            }

            return BadRequest("Developer not deleted");
        }

        private bool DeveloperExists(int id)
        {
            if (_repo.GetDeveloperById(id) != null)
            {
                return true;
            }
            return false;
        }
    }
}
