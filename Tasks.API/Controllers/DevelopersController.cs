using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasks.Domain._Common.Dtos;
using Tasks.Domain._Common.Results;
using Tasks.Domain.Developers.Dtos;
using Tasks.Domain.Developers.Services;

namespace Tasks.API.Controllers
{
    public class DevelopersController : TasksControllerBase
    {
        private readonly IDeveloperService _developerService;

        public DevelopersController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        [HttpGet("{id}")]
        public async Task<Result<DeveloperDetailDto>> GetDeveloperAsync([FromRoute] Guid id)
        {
            return GetResult(await _developerService.GetDeveloperByIdAsync(id));
        }

        [HttpGet]
        public async Task<Result<IEnumerable<DeveloperListDto>>> ListDevelopersAsync([FromQuery] PaginationDto paginationDto)
        {
            return GetResult(await _developerService.ListDevelopersAsync(paginationDto));
        }

        [HttpPost]
        public async Task<Result> CreateDeveloperAsync([FromBody] DeveloperCreateDto developerDto)
        {
            return GetResult(await _developerService.CreateDeveloperAsync(developerDto));
        }

        [HttpPut("{id}")]
        public async Task<Result> UpdateDeveloperAsync([FromBody] DeveloperUpdateDto developerDto, [FromRoute] Guid id)
        {
            developerDto.Id = id;
            return GetResult(await _developerService.UpdateDeveloperAsync(developerDto));
        }

        [HttpDelete("{id}")]
        public async Task<Result> DeleteDeveloperAsync([FromRoute] Guid id) 
        { 
            return GetResult(await _developerService.DeleteDeveloperAsync(id));
        }
    }
}
