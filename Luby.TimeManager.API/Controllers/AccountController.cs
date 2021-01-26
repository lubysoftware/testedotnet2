using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Application.Interfaces.Services.Domain;
using DTO.Request;
using DTO.Response;
using AutoMapper;
using Domain.Entities;
using Application.Interfaces.Services.Util;

namespace Luby.TimeManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IDeveloperService _developerService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;


        public AccountController(ILogger<AccountController> logger, IConfiguration config, IDeveloperService developerService, ITokenService tokenService, IMapper mapper) : base(logger, config)
        {
            _mapper = mapper;
            _developerService = developerService;
            _tokenService = tokenService;
            _config = config;
        }

        /// <summary>
        /// Autenticação de usuários.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Dados da autenticação</returns>
        /// <response code="200">the response object</response>  
        /// <response code="400">Caso precise de ajuste no request</response>  
        [HttpPost("login")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<object>> Login([FromBody] AuthUserRequestDTO user)
        {
            _logger.LogInformation("Validando usuário");

            var authUser = await _developerService.Auth(user.Cpf, user.Password);

            if (authUser == null)
            {
                return BadRequest(new AuthUserResponseDTO
                {
                    Authenticated = false,
                    Created = "",
                    Expiration = "",
                    AccessToken = "",
                    Message = "Usuário ou senha incorretos"
                });
            }

            _logger.LogInformation("Gerando token");
            var token = _tokenService.GenerateToken(authUser);

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao +
                TimeSpan.FromSeconds(10000);

            var usuarioResponse = _mapper.Map<Developer, DeveloperResponseDTO>(authUser);

            var retorno = new AuthUserResponseDTO
            {
                Authenticated = true,
                Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                Developer = usuarioResponse,
                Message = "OK"
            };
            return Ok(retorno);
        }
    }
}
