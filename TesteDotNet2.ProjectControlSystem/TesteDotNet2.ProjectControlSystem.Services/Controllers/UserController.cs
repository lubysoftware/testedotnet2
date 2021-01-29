using Microsoft.AspNetCore.Mvc;
using TesteDotNet2.ProjectControlSystem.Domain.Entities.Authentication;
using TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Service;

namespace TesteDotNet2.ProjectControlSystem.Services.Controllers
{
    public class UserController : BaseController
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("user/authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Usuário ou senha invalida" });

            return Ok(response);
        }
    }
}
