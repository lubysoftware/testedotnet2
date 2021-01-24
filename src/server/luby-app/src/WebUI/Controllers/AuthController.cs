using luby_app.Application.Auth.Commands.Login;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace luby_app.WebUI.Controllers
{
    public class AuthController : ApiControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<string>> Login(LoginCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.succeeded)
                return Ok(result.token);
            else
                return BadRequest("Login inválido! Por favor, verifique se email e senha informado estão corretos.");
        }
    }
}
