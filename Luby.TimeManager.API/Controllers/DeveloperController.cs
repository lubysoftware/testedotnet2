using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Domain.Entities;
using Application.Interfaces.Services.Domain;
using DTO.Request;
using DTO.Response;
using DTO.Pagination;
using Newtonsoft.Json;
using AutoMapper;

namespace Luby.TimeManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeveloperController : BaseController
    {
        private readonly IDeveloperService _developerService;

        public DeveloperController(ILogger<DeveloperController> logger, IConfiguration config, IDeveloperService developerService) : base(logger, config)
        {
            _developerService = developerService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PaginatedList<Developer, DeveloperResponseDTO>> Get(int? pagina, int? itensPorPagina)
        {
            _logger.LogInformation("[Obtendo lista de developers] pagina: {0}  itensPorPagina: {1}", pagina, itensPorPagina);
            return await _developerService.GetAllPaginatedAsync(pagina, itensPorPagina);
        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<DeveloperResponseDTO> GetItem(Guid id)
        {
            _logger.LogInformation("[Obtendo Developer] Id: {0}", id);
            return await _developerService.GetByIdAsync(id);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DeveloperResponseDTO>> Post([FromBody] DeveloperRequestDTO model)
        {
            _logger.LogInformation("[Inserindo Developer] Id: {0}", JsonConvert.SerializeObject(model));
            if (!await _developerService.IsValidCPF(model.Cpf))
                return BadRequest("Cpf Não autorizado");


            var obj = await _developerService.AddAsync(model);
            return Created(InsertedPath(obj.Id), obj);
        }

        [HttpPut]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(Guid id, [FromBody] DeveloperRequestDTO model)
        {
            _logger.LogInformation("[Alterando Developer] {0}", JsonConvert.SerializeObject(model));
            await _developerService.UpdateAsync(id, model);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(Guid id)
        {
            _logger.LogInformation("[Deletando Developer] id: {0}", id);
            await _developerService.RemoveAsync(id);
            return NoContent();
        }

        [HttpGet]
        [Route("top5-spent-time")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<DeveloperResponseDTO>> Top5()
        {
            _logger.LogInformation("[Top5]");
            return await _developerService.GetTop5SpentTimeIdAsync();
        }
    }
}
