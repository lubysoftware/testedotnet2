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
using Newtonsoft.Json;
using Infrastructure.Repositories.Domain.Dapper;
using System;
using Application.Interfaces.Services.Util;

namespace Application.Services.Domain
{
    public class ProjectService : ServiceBase<Project, ProjectRequestDTO, ProjectResponseDTO>,
                                   IProjectService
    {
        private readonly ISpentTimeRepository _spentTimeRepository;

        private readonly IDeveloperRepository _developerRepository;

        private readonly IDeveloperDapperRepository _developerDapperRepository;

        private readonly IHttpClientService _httpClientService;

        public ProjectService(IProjectRepository repository, ILogger<ProjectService> logger, IMapper mapper, ISpentTimeRepository spentTimeRepository, IDeveloperRepository developerRepository, 
            IDeveloperDapperRepository developerDapperRepository, IHttpClientService httpClientService) : base(repository, logger, mapper)
        {
            _spentTimeRepository = spentTimeRepository;
            _developerRepository = developerRepository;
            _developerDapperRepository = developerDapperRepository;
            _httpClientService = httpClientService;
        }

        public virtual async Task<SpentTimeResponseDTO> AddSpentTimeAsync(Guid projectId, Guid developerId, SpentTimeRequestDTO obj)
        {
            _logger.LogInformation("[AddSpentTimeAsync {0}] {1}", obj.GetType(), JsonConvert.SerializeObject(obj));
            var spentTime = _mapper.Map<SpentTime>(obj);
            spentTime.ProjectId = projectId;
            spentTime.DeveloperId = developerId;
            var y = await _spentTimeRepository.AddAsync(spentTime);
            var response = _mapper.Map<SpentTimeResponseDTO>(y);
            await SendNotification(response);
            return response;
        }

        public virtual async Task AddDeveloperAsync(Guid projectId, Guid developerId)
        {
            _logger.LogInformation("[AddDeveloperAsync]");
            await _developerDapperRepository.AddDeveloperProjectAsync(projectId, developerId);
        }

        private async Task SendNotification(SpentTimeResponseDTO spentTime)
        {
            try
            {
                _logger.LogInformation("[SendNotification] {0}", JsonConvert.SerializeObject(spentTime));

                var result = await _httpClientService.GetAsync("https://run.mocky.io/v3/a1b59b8e-577d-4996-a4c5-56215907d9dd");
                _logger.LogInformation("[SendNotification result] {0}", result);
            }catch(Exception e)
            {
                _logger.LogError(e, "[SendNotification error]");
            }
        }


    }
}
