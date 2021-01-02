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
using TesteDotnet.Models.ViewModels;

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
        // Pagination Example: v1/Developers?pageNumber=2&pageSize=10
        [HttpGet]
        public async Task<ActionResult> GetDeveloper([FromQuery]PageParams pageParams)
        {
            var developers = await _repo.GetAllDevelopersAsync(pageParams);
            var developersResult = _mapper.Map<IEnumerable<DeveloperDto>>(developers);

            Response.AddPagination(developers.CurrentPage, developers.PageSize, developers.TotalPages, developers.TotalCount);

            return Ok(developersResult);
        }

        /// <summary>
        /// Return a unique developer with an ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Developers/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Developer>> GetDeveloper(int id)
        {
            var developer = await _repo.GetDeveloperByIdAsync(id);

            if (developer == null)
            {
                return NotFound();
            }

            return developer;
        }

        /// <summary>
        /// Get rank of the 5 developers in a week
        /// </summary>
        /// <returns></returns>
        [HttpGet("rank")]
        public async Task<ActionResult<WorkedHoursRank>> GetDeveloperWorkedHoursRank()
        {
            var workedHoursRank = await _repo.GetDeveloperRankAsync();

            if (workedHoursRank == null)
            {
                return NotFound();
            }

            return Ok(workedHoursRank);
        }

        /// <summary>
        /// Update a developer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="developerDto"></param>
        /// <returns></returns>
        // PUT: api/Developers/id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeveloper(int id, DeveloperDto developerDto)
        {
            var developer = _mapper.Map<Developer>(developerDto);
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
                bool devExists = await DeveloperExists(id);
                if (!devExists)
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
        /// <param name="developerDto"></param>
        /// <returns></returns>
        // POST: api/Developers
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PostDeveloper(DeveloperDto developerDto)
        {
            var developer = _mapper.Map<Developer>(developerDto);
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
        public async Task<IActionResult> DeleteDeveloper(int id)
        {
            var developer = await _repo.GetDeveloperByIdAsync(id);
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

        private async Task<bool> DeveloperExists(int id)
        {
            var developer = await _repo.GetDeveloperByIdAsync(id);
            if (developer == null)
            {
                return true;
            }
            return false;
        }
    }
}
