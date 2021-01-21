using luby_app.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace luby_app.WebUI.Controllers
{
    public class UserProfileResponse
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        public UserProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize] 
        public async Task<ActionResult<UserProfileResponse>> GetUserProfile()
        { 
            string userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            string role = User.Claims.First(c => c.Type == ClaimTypes.Role).Value; 
            var user = await _userManager.FindByIdAsync(userId); 

            if (user == null)
                return null; 
            return new UserProfileResponse() { Email = user.Email, UserName = user.UserName, Role = role };
        }
    }
}
