using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;
using TesteDotnet.Data;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Threading.Tasks;
using TesteDotnet.Helpers;
using AutoMapper;
using TesteDotnet.V1.Dtos;

namespace TesteDotnet.V1.Controllers
{
    /// <summary>
    /// Controller for Developers
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DevelopersController : ControllerBase
    {
        private readonly IRepository _repo;
        // HttpClient is intended to be instantiated once per application, rather than per-use.
        static readonly HttpClient client = new HttpClient();
        private IMapper _mapper { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public DevelopersController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Return all developers
        /// </summary>
        /// <returns></returns>
        // GET: api/Developers
        [HttpGet]
        public ActionResult GetDeveloper()
        {
            var developers = _repo.GetAllDevelopers();
            return Ok(_mapper.Map<IEnumerable<DeveloperDto>>(developers));
        }

        /// <summary>
        /// Return a unique developer with an ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Developers/id
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

        /// <summary>
        /// Update a developer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="developer"></param>
        /// <returns></returns>
        // PUT: api/Developers/id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult PutDeveloper(int id, DeveloperDto developer)
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

        /// <summary>
        /// Create a new Developer
        /// </summary>
        /// <param name="developer"></param>
        /// <returns></returns>
        // POST: api/Developers
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PostDeveloper(DeveloperDto developer)
        {
            if (!ValidateCpf.IsValidCpf(developer.CPF))
            {
                return BadRequest("Invalid CPF");
            };
            _repo.Add(developer);
            _repo.SaveChanges();

            try
            {
                HttpResponseMessage response = await client.GetAsync("https://run.mocky.io/v3/067108b3-77a4-400b-af07-2db3141e95c9");
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
        /// Delete a Developer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
