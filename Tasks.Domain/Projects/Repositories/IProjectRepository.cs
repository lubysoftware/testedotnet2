using System;
using System.Threading.Tasks;
using Tasks.Domain._Common.Interfaces;
using Tasks.Domain.Projects.Entities;

namespace Tasks.Domain.Projects.Repositories
{
    public interface IProjectRepository : IRepository<Project> {
        Task<bool> ExistByTitle(string title, Guid ignoreId = default);
        Task<bool> ExistDeveloperVinculated(Guid id, Guid developerId);
    }
}
