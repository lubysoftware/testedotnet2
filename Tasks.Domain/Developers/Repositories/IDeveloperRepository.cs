using System.Threading.Tasks;
using Tasks.Domain._Common.Interfaces;
using Tasks.Domain.Developers.Entities;

namespace Tasks.Domain.Developers.Repositories
{
    public interface IDeveloperRepository : IRepository<Developer> {
        Task<bool> ExistByLoginAsync(string login);
        Task<Developer> FindByLoginAsync(string login);
    }
}
