using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Luby.Domain.Interfaces;
using Luby.Domain.Models;

namespace Luby.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DesenvolvedorController : ControllerBase
    {
        private readonly ILogger<DesenvolvedorController> _logger;
        private readonly DesenvolvedorService _desenvolvedorService;
        private readonly IRepository<Desenvolvedor> _desenvolvedorRepository;

        public DesenvolvedorController(ILogger<DesenvolvedorController> logger, DesenvolvedorService desenvolvedorService, IRepository<Desenvolvedor> desenvolvedorRepository)
        {
            _logger = logger;
            _desenvolvedorService = desenvolvedorService;
            _desenvolvedorRepository = desenvolvedorRepository;
        }

        /// <summary>
        /// Obter todos os desenvolvedores
        /// </summary>
        /// <returns></returns>
        /// <response code="200">A lista de desenvolvedores foi obtida com sucesso!</response>
        /// <response code="500">Ocorreu um erro ao obter a lista.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Desenvolvedor>),200)]
        [ProducesResponseType(500)]
        public ActionResult<List<Desenvolvedor>> Get()
        {
            var desenvolvedores = _desenvolvedorRepository.GetAll();
            var result=new List<Desenvolvedor>();
            foreach (var item in desenvolvedores)
            {
                result.Add(_desenvolvedorRepository.ConverterParaDominio(item));
            }
            return result;
        }

        [HttpGet("{id}")]
        public ActionResult<Desenvolvedor> GetById(int id)
        {

            var desenvolvedor = _desenvolvedorRepository.GetById(id);
            if (desenvolvedor == null)
            {
                return NotFound(new { message = $"Desenvolvedor de id={id} não encontrado" });
            }
            return desenvolvedor;
        }

        [HttpPost]
        public ActionResult<Desenvolvedor> Create(Desenvolvedor desenvolvedor)
        {
            if (desenvolvedor == null)
            {
                return BadRequest();
            }

            _desenvolvedorService.Save(desenvolvedor.Id, desenvolvedor.Nome, desenvolvedor.Cpf, desenvolvedor.Cargo, desenvolvedor.Email, desenvolvedor.Login, desenvolvedor.Senha);
            return Ok();
        }
        [HttpPut("{id}")]
        public ActionResult<Desenvolvedor> Update(Desenvolvedor desenvolvedor)
        {
            if (desenvolvedor == null)
            {
                return BadRequest();
            }
            if (_desenvolvedorRepository.GetById(desenvolvedor.Id) == null)
            {
                return NoContent();
            }
            _desenvolvedorService.Save(desenvolvedor.Id, desenvolvedor.Nome, desenvolvedor.Cpf, desenvolvedor.Cargo, desenvolvedor.Email, desenvolvedor.Login, desenvolvedor.Senha);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            if (_desenvolvedorRepository.GetById(id) == null)
                return NoContent();
            _desenvolvedorService.Delete(id);
            return Ok();
        }
        
    }
}