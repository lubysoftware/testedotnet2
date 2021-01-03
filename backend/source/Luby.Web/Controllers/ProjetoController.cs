using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Luby.Domain.Interfaces;
using Luby.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Luby.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjetoController : ControllerBase
    {

        private readonly ILogger<ProjetoController> _logger;
        private readonly ProjetoService _projetoService;
        private readonly IRepository<Projeto> _projetoRepository;

        public ProjetoController(ILogger<ProjetoController> logger, ProjetoService projetoService, IRepository<Projeto> projetoRepository)
        {
            _logger = logger;
            _projetoRepository = projetoRepository;
            _projetoService = projetoService;
        }

        /// <summary>
        /// Obter todos os projetos
        /// </summary>
        /// <returns></returns>
        /// <response code="200">A lista de projetos foi obtida com sucesso!</response>
        /// <response code="500">Ocorreu um erro ao obter a lista.</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<Projeto>), 200)]
        [ProducesResponseType(500)]
        public ActionResult<List<Projeto>> Get()
        {
            var projetos = _projetoRepository.GetAll();
            var result = new List<Projeto>();
            foreach (var item in projetos)
            {
                var prj = new Projeto(item.Nome);
                result.Add(prj);
            }
            return result;
        }

        [HttpGet("{id}")]
        public ActionResult<Projeto> GetById(int id)
        {

            var projeto = _projetoRepository.GetById(id);
            if (projeto == null)
            {
                return NotFound(new { message = $"Projeto de id={id} não encontrado" });
            }
            return projeto;
        }

        [HttpPost]
        public ActionResult<Desenvolvedor> Create(Projeto projeto)
        {
            if (projeto == null)
            {
                return BadRequest();
            }

            _projetoService.Save(projeto.Id,projeto.Nome);
            return Ok();
        }
    }
}