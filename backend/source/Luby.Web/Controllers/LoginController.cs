using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Luby.Domain.Interfaces;
using Luby.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Luby.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class LoginController : ControllerBase
    {
        private readonly ILogger<DesenvolvedorController> _logger;
        public IConfiguration _config;
        private readonly DesenvolvedorService _desenvolvedorService;
        private readonly IRepository<Desenvolvedor> _desenvolvedorRepository;
        public LoginController(ILogger<DesenvolvedorController> logger, IConfiguration Configuration, DesenvolvedorService desenvolvedorService, IRepository<Desenvolvedor> desenvolvedorRepository)
        {
            _logger = logger;
            _config = Configuration;
            _desenvolvedorService = desenvolvedorService;
            _desenvolvedorRepository = desenvolvedorRepository;
        }

        /// <summary>
        /// Obter todos os desenvolvedores
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Reorna a chave de acesso</response>
        /// <response code="500">Ocorreu um erro ao obter a chave.</response>
        [HttpPost]
        [ProducesResponseType(typeof(List<Desenvolvedor>), 200)]
        [ProducesResponseType(500)]
        public ActionResult Login(string login, string senha)
        {
            bool resultado = ValidarUsuario(login, senha);
            if (resultado)
            {
                var tokenString = GerarTokenJWT();
                return Ok(new { token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }

        private string GerarTokenJWT()
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(5);//expira em 5 minutos
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: issuer, audience: audience, expires: DateTime.Now.AddMinutes(5), signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

        private bool ValidarUsuario(string login, string senha)
        {
            if (_desenvolvedorService.Login(login, senha))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}