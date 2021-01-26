using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain.Standard;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories.Domain
{
    public interface IProjectRepository : IDomainRepository<Project>
    {
        Task<List<Project>> GetByDeveloperIdAsync(Guid DeveloperId);
    }
}
