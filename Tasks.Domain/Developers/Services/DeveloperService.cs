using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasks.Domain._Common.Dtos;
using Tasks.Domain._Common.Results;
using Tasks.Domain.Developers.Dtos;
using Tasks.Domain.Developers.Repositories;
using Tasks.Domain.External.Services;

namespace Tasks.Domain.Developers.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IMockyService _mockyService;
        public DeveloperService(
            IDeveloperRepository developerRepository,
            IMockyService mockyService
        ) { 
            _developerRepository = developerRepository;
        }

        public Task<Result> CreateDeveloperAsync(DeveloperCreateDto developerDto)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteDeveloperAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<DeveloperDetailDto> GetDeveloperByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DeveloperListDto>> ListDevelopersAsync(PaginationDto pagination)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateDeveloperAsync(DeveloperUpdateDto developerDto)
        {
            throw new NotImplementedException();
        }
    }
}
