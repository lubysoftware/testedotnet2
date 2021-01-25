using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Desafio.API.Models;
using Desafio.Business.DTO;
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
    public class LancamentoHorasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILancamentoHorasService _lancamentoHorasService;
        private readonly ILancamentoHorasRepository _lancamentoHorasRepository;


        public LancamentoHorasController(ILancamentoHorasService LancamentoHorasService, ILancamentoHorasRepository LancamentoHorasRepository, IMapper mapper)
        {
            _lancamentoHorasService = LancamentoHorasService;
            _lancamentoHorasRepository = LancamentoHorasRepository;
            _mapper = mapper;
        }


        [HttpGet("ObterTodos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LancamentoHorasViewModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<LancamentoHorasViewModel>>> ObterTodos()
        {
            try
            {
                var listaLancamentos = await _lancamentoHorasService.BuscarTodos();

                return Ok(_mapper.Map<IEnumerable<LancamentoHorasViewModel>>(listaLancamentos));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("ObterPorId/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LancamentoHorasViewModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LancamentoHorasViewModel>> ObterPorId(int id)
        {
            LancamentoHorasViewModel model;

            try
            {
                var lancamentoHoras = await _lancamentoHorasService.BuscarPorID(id);

                if (lancamentoHoras == null) return NotFound();

                model = _mapper.Map<LancamentoHorasViewModel>(lancamentoHoras);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return Ok(model);
        }


        [HttpPost("Adicionar")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(LancamentoHorasViewModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LancamentoHorasViewModel>> Adicionar(LancamentoHorasViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var lancamentoHoras = _mapper.Map<LancamentoHoras>(model);

            try
            {
                await _lancamentoHorasService.Adicionar(lancamentoHoras);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(ObterPorId), new { id = lancamentoHoras.Id }, model);
        }


        [HttpPut("Atualizar")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Atualizar(LancamentoHorasViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (!_lancamentoHorasRepository.Buscar(p => p.Id == model.Id).Result.Any())
            {
                return NotFound();
            }

            try
            {
                var lancamentoHoras = _mapper.Map<LancamentoHoras>(model);

                await _lancamentoHorasService.Atualizar(lancamentoHoras);
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
        public async Task<ActionResult<LancamentoHoras>> Excluir(int id)
        {
            if (!_lancamentoHorasRepository.Buscar(p => p.Id == id).Result.Any())
            {
                return NotFound();
            }

            try
            {
                await _lancamentoHorasService.Remover(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return NoContent();
        }


        [HttpGet("RanqueamentoDaSemana")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RanqueamentoDTO>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<RanqueamentoDTO>>> RanqueamentoDaSemana()
        {
            try
            {
                return Ok(await _lancamentoHorasService.RanqueamentoDaSemana());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }


}