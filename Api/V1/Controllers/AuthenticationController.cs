using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using TesteDotnet.Data;
using TesteDotnet.Services;

namespace TesteDotnet.V1.Controllers
{
    /// <summary>
    /// Controller for Athentication and Token Generation
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private IRepository _repo { get; set; }
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="configuration"></param>
        public AuthenticationController(IRepository repository, IConfiguration configuration)
        {
            _repo = repository;
            Configuration = configuration;
        }

        /// <summary>
        /// Login into application
        /// </summary>
        /// <param name="developerLogin"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] DeveloperLogin developerLogin)
        {
            Developer developer = await _repo.GetDeveloperLoginAsync(developerLogin.Email, developerLogin.Password);
            TokenService tokenService = new TokenService(Configuration);

            if (developer == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = tokenService.GenerateToken(developer);

            developer.Password = "";

            return new
            {
                developer = developer,
                token = token
            };
        }
    }

    /// <summary>
    /// ViewModel for Login
    /// </summary>
    public class DeveloperLogin
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
