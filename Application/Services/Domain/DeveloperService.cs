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
using Application.Interfaces.Services.Util;
using Newtonsoft.Json;

namespace Application.Services.Domain
{
    public class DeveloperService : ServiceBase<Developer, DeveloperRequestDTO, DeveloperResponseDTO>, 
                                   IDeveloperService
    {
        private readonly IDeveloperDapperRepository _developerDapperRepository;
        private readonly IDeveloperRepository _repository;
        private readonly IHttpClientService _httpClientService;
        private readonly IProjectRepository _projectRepository;

        public DeveloperService(IDeveloperRepository repository, ILogger<DeveloperService> logger, IMapper mapper, IDeveloperDapperRepository developerDapperRepository, IProjectRepository projectRepository, IHttpClientService httpClientService) : base(repository, logger, mapper)
        {
            _developerDapperRepository = developerDapperRepository;
            _repository = repository;
            _httpClientService = httpClientService;
            _projectRepository = projectRepository;
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

            var projects = await _projectRepository.GetByDeveloperIdAsync(developerId);

            return projects.Where(x=> x.Id == projectId).Count() > 0;
        }

        public virtual async Task<bool> IsValidCPF(string cpf)
        {
            _logger.LogInformation("[CanAddSpentTimeAsync]");

            var isValidCPF = await _httpClientService.GetAsync("https://run.mocky.io/v3/067108b3-77a4-400b-af07-2db3141e95c9");

            var obj = JsonConvert.DeserializeObject<IsValidCPFResponseDTO>(isValidCPF);

            if (obj.Message != "Autorizado")
            {
                return false;
            }

            return true;
        }
    }
}
