using Application.Interfaces.Services.Standard;
using Domain.Entities;
using DTO.Request;
using DTO.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Domain
{
    public interface IProjectService : IServiceBase<Project, ProjectRequestDTO, ProjectResponseDTO>
    {
        Task<SpentTimeResponseDTO> AddSpentTimeAsync(Guid projectId, Guid developerId, SpentTimeRequestDTO obj);
        Task AddDeveloperAsync(Guid projectId, Guid developerId);
    }
}
