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

namespace Application.Services.Domain
{
    public class ProjectService : ServiceBase<Project, ProjectRequestDTO, ProjectResponseDTO>, 
                                   IProjectService
    {
        private readonly ISpentTimeRepository _spentTimeRepository;

        public ProjectService(IProjectRepository repository, ILogger<ProjectService> logger, IMapper mapper, ISpentTimeRepository spentTimeRepository) : base(repository, logger, mapper)
        {
            _spentTimeRepository = spentTimeRepository;
        }

        public virtual async Task<SpentTimeResponseDTO> AddSpentTimeAsync(Guid projectId, Guid developerId, SpentTimeRequestDTO obj)
        {
            _logger.LogInformation("[AddSpentTimeAsync {0}] {1}", obj.GetType(), JsonConvert.SerializeObject(obj));
            var spentTime = _mapper.Map<SpentTime>(obj);
            spentTime.ProjectId = projectId;
            spentTime.DeveloperId = developerId;
            var y = await _spentTimeRepository.AddAsync(spentTime);
            var response = _mapper.Map<SpentTimeResponseDTO>(y);
            return response;
        }
    }
}
