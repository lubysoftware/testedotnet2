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
using TesteDotnet.Dtos;

namespace TesteDotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private readonly IRepository _repo;
        // HttpClient is intended to be instantiated once per application, rather than per-use.
        static readonly HttpClient client = new HttpClient();
        private IMapper _mapper { get; }

        public DevelopersController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/Developers
        [HttpGet]
        public ActionResult GetDeveloper()
        {
            var developers = _repo.GetAllDevelopers();
            return Ok(_mapper.Map<IEnumerable<DeveloperDto>>(developers));
        }

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
        public async Task<ActionResult> PostDeveloper(Developer developer)
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
