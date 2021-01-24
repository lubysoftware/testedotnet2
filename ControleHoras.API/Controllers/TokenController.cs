using ControleHoras.API.AppModels;
using ControleHoras.API.BaseModels;
using ControleHoras.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleHoras.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class TokenController : ApiControllerBase
    {
        [HttpPost]
        public IActionResult Post(User user)
        {
            return Ok(TokenService.Instance.GenerateToken(user));
        }
    }
}