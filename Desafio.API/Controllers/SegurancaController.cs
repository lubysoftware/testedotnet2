using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desafio.API.Models;
using Desafio.Business.Interfaces;
using Desafio.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Desafio.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SegurancaController : ControllerBase
    {
        private readonly IDesenvolvedorRepository _desenvolvedorRepository;

        private readonly IConfiguration _config;


        public SegurancaController(IDesenvolvedorRepository desenvolvedorRepository, IConfiguration configuration)
        {
            _desenvolvedorRepository = desenvolvedorRepository;
            _config = configuration;

        }



        [HttpPost("Entrar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenViewModel))]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            bool resultado = ValidarUsuario(model);

            if (resultado)
            {
                var tokenString = GerarTokenJwt();

                return Ok(new TokenViewModel { Token = tokenString });
            }
            else
            {
                return BadRequest("Usuário e senha inválidos");
            }
        }
        private bool ValidarUsuario(LoginViewModel model)
        {
            if (_desenvolvedorRepository.Buscar(x => x.Usuario == model.Usuario && x.Senha == model.Senha).Result.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string GerarTokenJwt()
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(5);

            var securityKey = new SymmetricSecurityKey
                              (Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var credentials = new SigningCredentials
                              (securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: issuer,
                                             audience: audience,
                                             expires: expiry,
                                             signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }

    }


}