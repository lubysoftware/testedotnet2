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
using System;
using System.Linq;

namespace Application.Services.Domain
{
    public class DeveloperService : ServiceBase<Developer, DeveloperRequestDTO, DeveloperResponseDTO>, 
                                   IDeveloperService
    {
        private readonly IDeveloperDapperRepository _developerDapperRepository;
        private readonly IDeveloperRepository _repository;

        public DeveloperService(IDeveloperRepository repository, ILogger<DeveloperService> logger, IMapper mapper, IDeveloperDapperRepository developerDapperRepository) : base(repository, logger, mapper)
        {
            _developerDapperRepository = developerDapperRepository;
            _repository = repository;
        }

        


        public virtual async Task<IEnumerable<DeveloperResponseDTO>> GetTop5SpentTimeIdAsync()
        {
            _logger.LogInformation("[GetTop5SpentTimeIdAsync]");
            var y = await _developerDapperRepository.GetTop5SpentTimeIdAsync();
            var response = _mapper.Map< IEnumerable<DeveloperResponseDTO>>(y);
            return response;
        }


        public async Task<Developer> Auth(string email, string password)
        {
            var user = await GetByEmail(email);
            if (user == null)
                return null;
            if (user.Password != password)
                return null;
            return user;
        }

        public async Task<Developer> GetByEmail(string email)
        {
            var user = await _repository.GetByEmailAsync(email);
            return user;
        }

        public virtual async Task<bool> CanAddSpentTimeAsync(Guid projectId, Guid developerId)
        {
            _logger.LogInformation("[CanAddSpentTimeAsync]");

            var developer = await _repository.GetByIdAsync(developerId);
            if (developer is null)
            {
                return false;
            }

            var project = developer.Projects.Where(x => x.Id == projectId).FirstOrDefault();

            return project != null;
        }
    }
}
