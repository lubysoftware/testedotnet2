using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasks.Domain._Common.Dtos;
using Tasks.Domain._Common.Results;
using Tasks.Domain.Developers.Dtos;

namespace Tasks.Domain.Developers.Services
{
    public interface IDeveloperService
    {
        Task<Result<DeveloperDetailDto>> GetDeveloperByIdAsync(Guid id);
        Task<IEnumerable<DeveloperListDto>> ListDevelopersAsync(PaginationDto pagination);
        Task<Result> CreateDeveloperAsync(DeveloperCreateDto developerDto);
        Task<Result> UpdateDeveloperAsync(DeveloperUpdateDto developerDto);
        Task<Result> DeleteDeveloperAsync(Guid id);
    }
}
