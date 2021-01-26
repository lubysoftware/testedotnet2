using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Repositories.Domain; 
 using Microsoft.Extensions.Logging;
using Infrastructure.Repositories.Standard.EFCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;

namespace Infrastructure.Repositories.Domain.EFCore
{
    public class DeveloperRepository : DomainRepository<Developer>,
                                      IDeveloperRepository
    {
        public DeveloperRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<Developer> GetByEmailAsync(string cpf)
        {
            IQueryable<Developer> query = await Task.FromResult(GenerateQuery(filter: developer => developer.Cpf == cpf));
            return query.SingleOrDefault();
        }
    }
}
