using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Infra;
using App.DAL;
using api.ViewModels;
using api.Services;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.Swagger.Annotations;
using System.Web.Http.ModelBinding;
using System.Net;
using Microsoft.AspNetCore.Http;

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
		/// <summary>
		/// Autenticar usuário 
		/// </summary>
		/// <param name="login"></param>
		/// <returns>dados do usuário e o token</returns>
		/// <response code="200">dados do usuário e o token</response>
		/// <response code="400">Mensagens de erro</response>
		[ProducesResponseType(typeof(DeveloperLogged), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
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
				return Ok(new DeveloperLogged(new DeveloperView(user),jwt));
			}
			return ValidationProblem();
		}
	}
}
