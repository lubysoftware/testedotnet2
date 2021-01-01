using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Domain.Projects.Entities;
using Tasks.Domain.Projects.Repositories;
using Tasks.Ifrastructure._Common.Repositories;
using Tasks.Ifrastructure.Contexts;

namespace Tasks.Ifrastructure.Repositories.Projects
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(TasksContext context) : base(context) { }

        public async Task<bool> ExistByTitle(string title, Guid ignoreId = default)
        {
            return await _context.Projects.AnyAsync(p => p.Title == title && p.Id != ignoreId);
        }

        public async Task<bool> ExistDeveloperVinculated(Guid id, Guid developerId)
        {
            return await _context.Projects.AnyAsync(p => p.Id == id && p.DeveloperProjects.Any(dp => dp.DeveloperId == developerId));
        }
    }
}
