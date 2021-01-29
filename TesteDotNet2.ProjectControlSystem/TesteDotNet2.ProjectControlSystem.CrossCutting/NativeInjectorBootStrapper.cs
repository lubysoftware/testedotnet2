using Microsoft.Extensions.DependencyInjection;
using TesteDotNet2.ProjectControlSystem.Data.SqlServer.Context;
using TesteDotNet2.ProjectControlSystem.Data.SqlServer.Repository;
using TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Repository;
using TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Service;
using TesteDotNet2.ProjectControlSystem.Domain.Services;

namespace TesteDotNet2.ProjectControlSystem.CrossCutting
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain - Services
            services.AddScoped<IDeveloperService, DeveloperService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITimeSheetService, TimeSheetService>();
            services.AddScoped<IUserService, UserService>();

            // Infra - Data
            services.AddScoped<IDeveloperRepository, DeveloperRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITimeSheetRepository, TimeSheetRepository>();

            services.AddScoped<ProjectControlSystemContext>();


        }
    }
}
