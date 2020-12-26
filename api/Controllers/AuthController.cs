using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Infra;
using App.DAL;
using api.ViewModels;
using api.Services;

namespace api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthController : Controller
	{
		private readonly DAODeveloper _dao;
		private readonly JwtService _jwtService;

		public AuthController(Context context, JwtService jwtService)
		{
			_dao = new DAODeveloper(context);
			_jwtService = jwtService;
		}

		[HttpPost, AllowAnonymous, Route("")]
		public IActionResult Authenticate([FromBody] LoginModel login)
		{
            if (ModelState.IsValid)
			{
				var user = _dao.GetByEmail(login.Email);
                if (user == null)
                {
					ModelState.AddModelError("Email", "Email not registered");
					return ValidationProblem();
				}
                if (HashService.Decrypt(user.Password).CompareTo(login.Password) != 0)
                {
					ModelState.AddModelError("Password", "Invalid password");
					return ValidationProblem();
				}
				var jwt = _jwtService.GenerateToken(user);
				return Ok(new
				{
					developer = new DeveloperView(user),
					token = jwt
				});
			}
			return ValidationProblem();
		}
	}
}
