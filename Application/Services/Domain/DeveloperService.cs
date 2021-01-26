using Application.Interfaces.Services.Domain;
using Application.Services.Standard;
using Domain.Entities;
using DTO.Request;
using DTO.Response;
using AutoMapper;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Interfaces.Repositories.Domain;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Domain
{
    public class DeveloperService : ServiceBase<Developer, DeveloperRequestDTO, DeveloperResponseDTO>, 
                                   IDeveloperService
    {
        private readonly IDeveloperDapperRepository _developerDapperRepository;

        public DeveloperService(IDeveloperRepository repository, ILogger<DeveloperService> logger, IMapper mapper, IDeveloperDapperRepository developerDapperRepository) : base(repository, logger, mapper)
        {
            _developerDapperRepository = developerDapperRepository;
        }

        


        public virtual async Task<IEnumerable<DeveloperResponseDTO>> GetTop5SpentTimeIdAsync()
        {
            _logger.LogInformation("[GetTop5SpentTimeIdAsync]");
            var y = await _developerDapperRepository.GetTop5SpentTimeIdAsync();
            var response = _mapper.Map< IEnumerable<DeveloperResponseDTO>>(y);
            return response;
        }
    }
}
