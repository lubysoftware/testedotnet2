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
    public class ProjetoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjetoService _projetoService;
        private readonly IProjetoRepository _projetoRepository;
        private readonly IDesenvolvedorService _desenvolvedorService;



        public ProjetoController(IProjetoService projetoService, IProjetoRepository projetoRepository, IDesenvolvedorService desenvolvedorService, IMapper mapper)
        {
            _projetoService = projetoService;
            _projetoRepository = projetoRepository;
            _desenvolvedorService = desenvolvedorService;
            _mapper = mapper;
        }


        [HttpGet("ObterTodos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProjetoViewModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProjetoViewModel>>> ObterTodos()
        {
            try
            {
                var listaProjetos = await _projetoService.BuscarTodos();
                return Ok(_mapper.Map<IEnumerable<ProjetoViewModel>>(listaProjetos));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObterTodosPaginado")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProjetoViewModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProjetoViewModel>>> ObterTodosPaginado([FromQuery] int pagina, [FromQuery] int qtdRegistros)
        {

            try
            {
                var listaProjetos = await _projetoService.BuscarTodosPaginado(pagina, qtdRegistros);

                return Ok(_mapper.Map<IEnumerable<ProjetoViewModel>>(listaProjetos));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObterPorId/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjetoViewModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProjetoViewModel>> ObterPorId(int id)
        {
            ProjetoViewModel model;

            try
            {
                var projeto  = await _projetoService.BuscarPorID(id);

                if (projeto == null) return NotFound();

                model = _mapper.Map<ProjetoViewModel>(projeto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



            return Ok(model);
        }


        [HttpPost("Adicionar")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProjetoViewModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProjetoViewModel>> Adicionar(ProjetoViewModel model)
        {

            if (!ModelState.IsValid) return BadRequest();


            var projeto = _mapper.Map<Projeto>(model);


            try
            {
                await _projetoService.Adicionar(projeto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(ObterPorId), new { id = projeto.Id }, model);
        }

        [HttpPost("VincularDesenvolvedor")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Vincular([FromQuery] int projetoID, [FromQuery] int desenvolvedorID)
        {

            try
            {

                 await _projetoService.AdicionarDesenvolvedor(projetoID, desenvolvedorID);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Accepted();
        }


        [HttpPut("Atualizar")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Atualizar(ProjetoViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();


            if (!_projetoRepository.Buscar(p => p.Id == model.Id).Result.Any())
            {
                return NotFound();
            }

            try
            {
                var projeto = _mapper.Map<Projeto>(model);

                await _projetoService.Atualizar(projeto);
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
        public async Task<IActionResult> Excluir(int id)
        {
            if (!_projetoRepository.Buscar(p => p.Id == id).Result.Any())
            {
                return NotFound();
            }

            try
            {
                await _projetoService.Remover(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return NoContent();
        }



    }


}