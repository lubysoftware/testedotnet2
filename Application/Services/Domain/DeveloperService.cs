using Application.Interfaces.Services.Domain;
using Application.Services.Standard;
using Domain.Entities;
using DTO.Request;
using DTO.Response;
using AutoMapper;
using Infrastructure.Interfaces.Repositories.Domain; 
 using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Domain
{
    public class DeveloperService : ServiceBase<Developer, DeveloperRequestDTO, DeveloperResponseDTO>, 
                                   IDeveloperService
    {
        private readonly IDeveloperRepository _repository;

        public DeveloperService(IDeveloperRepository repository, ILogger<DeveloperService> _logger, IMapper mapper) : base(repository, _logger, mapper)
        {
            _repository = repository;
        }
    }
}
