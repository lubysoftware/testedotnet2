using luby_app.Application.Usuario.Queries.GetUserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace luby_app.WebUI.Controllers
{
    [Authorize]
    public class UserProfileController : ApiControllerBase
    {  
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserProfileDto>> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return await Mediator.Send(new GetUserProfileQuery() { UserId = userId  });
        }
    }
}
