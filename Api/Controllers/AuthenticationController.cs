using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TesteDotnet.Data;
using TesteDotnet.Services;

namespace TesteDotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IRepository _repo { get; set; }
        public IConfiguration Configuration { get; }

        public AuthenticationController(IRepository repository, IConfiguration configuration)
        {
            _repo = repository;
            Configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Authenticate([FromBody] DeveloperLogin developerLogin)
        {
            Developer developer = _repo.GetDeveloperLogin(developerLogin.Email, developerLogin.Password);
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

    public class DeveloperLogin
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
