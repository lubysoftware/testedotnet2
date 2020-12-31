using Microsoft.Extensions.DependencyInjection;
using Tasks.Domain.Developers.Services;
using Tasks.Domain.External.Services;
using Tasks.Domain.Projects.Services;
using Tasks.Service.Developers;
using Tasks.Service.External;
using Tasks.Service.Projects;

namespace Tasks.CrossCutting
{
    public static class ConfigureServices
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMockyService, MockyService>();
            services.AddScoped<IDeveloperService, DeveloperService>();
            services.AddScoped<IProjectService, ProjectService>();
        }
    }
}
