using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;
using TesteDotnet.Data;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Net.Http;

namespace TesteDotnet.V1.Controllers
{
    /// <summary>
    /// Controller for Entries
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EntriesController : ControllerBase
    {
        public readonly IRepository _repo;
        // HttpClient is intended to be instantiated once per application, rather than per-use.
        static readonly HttpClient client = new HttpClient();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        public EntriesController(IRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Get all entries
        /// </summary>
        /// <returns></returns>
        // GET: api/Entries
        [HttpGet]
        public ActionResult<IEnumerable<Entry>> GetEntry()
        {
            return _repo.GetAllEntries();
        }

        /// <summary>
        /// Get an Entry with an ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Entries/5
        [HttpGet("{id}")]
        public ActionResult<Entry> GetEntry(int id)
        {
            var entry = _repo.GetEntryById(id);

            if (entry == null)
            {
                return NotFound();
            }

            return entry;
        }

        /// <summary>
        /// Update an entry
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entry"></param>
        /// <returns></returns>
        // PUT: api/Entries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult PutEntry(int id, Entry entry)
        {
            if (id != entry.Id)
            {
                return BadRequest();
            }

            if (!_repo.IsDateAvailable(entry))
            {
                return BadRequest("Entry dates not available");
            }
            if (!_repo.DeveloperHasProject(entry.DeveloperId, entry.ProjectId))
            {
                return BadRequest("Developer are not associated to this project");
            }

            _repo.Update(entry);

            try
            {
                if (_repo.SaveChanges())
                {
                    return Ok(entry);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return BadRequest("Entry not updated");
        }

        /// <summary>
        /// Create an Entry
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        // POST: api/Entries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PostEntry(Entry entry)
        {
            if (!_repo.IsDateAvailable(entry))
            {
                return BadRequest("Entry dates not available");
            }
            if (!_repo.DeveloperHasProject(entry.DeveloperId, entry.ProjectId))
            {
                return BadRequest("Developer are not associated to this project");
            }

            _repo.Add(entry);
            _repo.SaveChanges();

            try
            {
                HttpResponseMessage response = await client.GetAsync("https://run.mocky.io/v3/a1b59b8e-577d-4996-a4c5-56215907d9dd");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return Ok(responseBody);
            }
            catch (HttpRequestException e)
            {
                return BadRequest(e.StatusCode + " " + e.HelpLink);
            }
        }

        /// <summary>
        /// Delete an Entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Entries/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteEntry(int id)
        {
            var entry = _repo.GetEntryById(id);
            if (entry == null)
            {
                return NotFound();
            }

            _repo.Delete(entry);
            if (_repo.SaveChanges())
            {
                return Ok(entry);
            }

            return BadRequest("Entry not deleted");
        }

        private bool EntryExists(int id)
        {
            if (_repo.GetEntryById(id) != null)
            {
                return true;
            }
            return false;
        }
    }
}
