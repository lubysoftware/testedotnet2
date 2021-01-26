using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain.Standard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories.Domain
{
    public interface IDeveloperRepository : IDomainRepository<Developer>
    {
        Task<Developer> GetByEmailAsync(string email);
    }
}
