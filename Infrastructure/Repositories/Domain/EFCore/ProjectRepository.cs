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
    public class ProjectRepository : DomainRepository<Project>,
                                      IProjectRepository
    {
        public ProjectRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Project>> GetByDeveloperIdAsync(Guid DeveloperId)
        {
            IQueryable<Project> query = await Task.FromResult(GenerateQuery(filter: project => project.Developers.Where(x=> x.Id == DeveloperId).Count() > 0, includeProperties: new string[] { nameof(Project.Developers)}));
            return query.AsEnumerable().ToList();
        }
    }
}
