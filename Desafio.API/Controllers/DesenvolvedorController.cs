using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Desafio.API.Models;
using Desafio.Business.Interfaces;
using Desafio.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Desafio.API.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class DesenvolvedorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDesenvolvedorService _desenvolvedorService;
        private readonly ILancamentoHorasService _lancamentoHorasService;
        private readonly IDesenvolvedorRepository _desenvolvedorRepository;


        public DesenvolvedorController(
            IDesenvolvedorService desenvolvedorService,
            IDesenvolvedorRepository desenvolvedorRepository,
            ILancamentoHorasService lancamentoHorasService,
            IMapper mapper)
        {
            _mapper = mapper;
            _desenvolvedorService = desenvolvedorService;
            _desenvolvedorRepository = desenvolvedorRepository;
            _lancamentoHorasService = lancamentoHorasService;
        }


        [HttpGet("ObterTodos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DesenvolvedorViewModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DesenvolvedorViewModel>>> ObterTodos()
        {
            try
            {
                var listaDesenvolvedores = await _desenvolvedorService.BuscarTodos();

                return Ok(_mapper.Map<IEnumerable<DesenvolvedorViewModel>>(listaDesenvolvedores));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObterTodosPaginado")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DesenvolvedorViewModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DesenvolvedorViewModel>>> ObterTodosPaginado([FromQuery] int pagina, [FromQuery] int qtdRegistros)
        {

            try
            {
                var listaDesenvolvedores = await _desenvolvedorService.BuscarTodosPaginado(pagina, qtdRegistros);
                return Ok(_mapper.Map<IEnumerable<DesenvolvedorViewModel>>(listaDesenvolvedores));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("ObterPorId/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DesenvolvedorViewModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DesenvolvedorViewModel>> ObterPorId(int id)
        {
            DesenvolvedorViewModel model;

            try
            {
                var desenvolvedor = await _desenvolvedorService.BuscarPorID(id);
                if (desenvolvedor == null) return NotFound();

                model = _mapper.Map<DesenvolvedorViewModel>(desenvolvedor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(model);
        }


        [HttpPost("Adicionar")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DesenvolvedorAdicionarViewModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Adicionar(DesenvolvedorAdicionarViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var desenvolvedor = _mapper.Map<Desenvolvedor>(model);
            try
            {
                await _desenvolvedorService.Adicionar(desenvolvedor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(ObterPorId), new { id = desenvolvedor.Id }, model);
        }


        [HttpPut("Atualizar")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Atualizar(DesenvolvedorViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (!_desenvolvedorRepository.Buscar(p => p.Id == model.Id).Result.Any())
            {
                return NotFound();
            }

            try
            {
                var desenvolvedor = _mapper.Map<Desenvolvedor>(model);
                await _desenvolvedorService.Atualizar(desenvolvedor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return Accepted(nameof(ObterPorId), new { id = model.Id });
        }


        [HttpDelete("Excluir/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Excluir(int id)
        {
            if (!_desenvolvedorRepository.Buscar(p => p.Id == id).Result.Any())
            {
                return NotFound();
            }

            try
            {
                await _desenvolvedorService.Remover(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return NoContent();
        }



    }


}