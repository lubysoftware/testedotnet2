using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain.Standard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories.Domain
{
    public interface IDeveloperDapperRepository : IDomainRepository<Developer>
    {
        Task<IEnumerable<Developer>> GetTop5SpentTimeIdAsync();
    }
}
