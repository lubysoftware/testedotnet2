using TesteDotNet2.ProjectControlSystem.Data.SqlServer.Context;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;
using TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Repository;

namespace TesteDotNet2.ProjectControlSystem.Data.SqlServer.Repository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(ProjectControlSystemContext context)
        : base(context)
        {

        }
    }
}
