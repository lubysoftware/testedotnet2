using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;
using TesteDotnet.Data;

namespace TesteDotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private readonly Context _context;
        public readonly IRepository _repo;

        public DevelopersController(Context context, IRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: api/Developers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Developer>>> GetDeveloper()
        {
            return await _context.Developer.ToListAsync();
        }

        // GET: api/Developers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Developer>> GetDeveloper(int id)
        {
            var developer = await _context.Developer.FindAsync(id);

            if (developer == null)
            {
                return NotFound();
            }

            return developer;
        }

        // PUT: api/Developers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // DELETE: api/Developers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeveloper(int id)
        {
            var developer = await _context.Developer.FindAsync(id);
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
            return _context.Developer.Any(e => e.Id == id);
        }
    }
}
