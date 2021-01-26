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
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly IDeveloperService _developerService;

        public ProjectController(ILogger<ProjectController> logger, IConfiguration config, IProjectService projectService, IDeveloperService developerService) : base(logger, config)
        {
            _projectService = projectService;
            _developerService = developerService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PaginatedList<Project, ProjectResponseDTO>> Get(int? pagina, int? itensPorPagina)
        {
            _logger.LogInformation("[Obtendo lista de projects] pagina: {0}  itensPorPagina: {1}", pagina, itensPorPagina);
            return await _projectService.GetAllPaginatedAsync(pagina, itensPorPagina);
        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ProjectResponseDTO> GetItem(Guid id)
        {
            _logger.LogInformation("[Obtendo Project] Id: {0}", id);
            return await _projectService.GetByIdAsync(id);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProjectResponseDTO>> Post([FromBody] ProjectRequestDTO model)
        {
            _logger.LogInformation("[Inserindo Project] Id: {0}", JsonConvert.SerializeObject(model));
            var obj = await _projectService.AddAsync(model);
            return Created(InsertedPath(obj.Id), obj);
        }

        [HttpPut]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(Guid id, [FromBody] ProjectRequestDTO model)
        {
            _logger.LogInformation("[Alterando Project] {0}", JsonConvert.SerializeObject(model));
            await _projectService.UpdateAsync(id, model);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(Guid id)
        {
            _logger.LogInformation("[Deletando Project] id: {0}", id);
            await _projectService.RemoveAsync(id);
            return NoContent();
        }

        [HttpPost]
        [Route("{id}/add-spent-time")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SpentTimeResponseDTO>> Post(Guid id, [FromBody] SpentTimeRequestDTO model)
        {
            _logger.LogInformation("[Adicionando tempo gasto] ProjectId: {0} Json: {1}", id, JsonConvert.SerializeObject(model));

            if (!await _developerService.CanAddSpentTimeAsync(id, CurrentDeveloperId))
                return BadRequest(new { Message = "O desenvolvedor não pode incluir tempo gasto no projeto informado" });

            var obj = await _projectService.AddSpentTimeAsync(id, CurrentDeveloperId, model);
            return Created(InsertedPath(obj.Id), obj);
        }

        [HttpPost]
        [Route("{id}/add-developer")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddDeveloper(Guid id, [FromBody] AddDeveloperToProjectRequestDTO model)
        {
            _logger.LogInformation("[Adicionando Developer] ProjectId: {0} Json: {1}", id, JsonConvert.SerializeObject(model));
            await _projectService.AddDeveloperAsync(id, model.DeveloperId);
            return NoContent();
        }
    }
}
