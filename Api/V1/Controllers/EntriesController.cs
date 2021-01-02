using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;
using TesteDotnet.Data;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Net.Http;
using TesteDotnet.V1.Dtos;
using AutoMapper;

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
        private IMapper _mapper { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public EntriesController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all entries
        /// </summary>
        /// <returns></returns>
        // GET: api/Entries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entry>>> GetEntry()
        {
            var entries = await _repo.GetAllEntriesAsync();
            return Ok(_mapper.Map<IEnumerable<EntryDto>>(entries));
        }

        /// <summary>
        /// Get an Entry with an ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Entries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entry>> GetEntry(int id)
        {
            var entry = await _repo.GetEntryByIdAsync(id);

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
        /// <param name="entryDto"></param>
        /// <returns></returns>
        // PUT: api/Entries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntry(int id, EntryDto entryDto)
        {
            var entry = _mapper.Map<Entry>(entryDto);
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
                bool entryExists = await EntryExists(id);
                if (!entryExists)
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
        /// <param name="entryDto"></param>
        /// <returns></returns>
        // POST: api/Entries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PostEntry(EntryDto entryDto)
        {
            var entry = _mapper.Map<Entry>(entryDto);
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
        public async Task<IActionResult> DeleteEntry(int id)
        {
            var entry = await _repo.GetEntryByIdAsync(id);
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

        private async Task<bool> EntryExists(int id)
        {
            var entry = await _repo.GetEntryByIdAsync(id);
            if (entry != null)
            {
                return true;
            }
            return false;
        }
    }
}
