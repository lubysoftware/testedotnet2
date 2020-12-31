using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasks.Domain._Common.External;
using Tasks.Domain._Common.Interfaces;
using Tasks.Domain._Common.Security;
using Tasks.Domain.Developers.Repositories;
using Tasks.Domain.Developers.Services;
using Tasks.Domain.External.Services;
using Tasks.Domain.Projects.Repositories;
using Tasks.Domain.Projects.Services;
using Tasks.Ifrastructure._Common.Repositories;
using Tasks.Ifrastructure.Contexts;
using Tasks.Ifrastructure.Repositories.Developers;
using Tasks.Ifrastructure.Repositories.Projects;

namespace Tasks.Ifrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDatabases(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TasksContext>(
                options => options.UseMySql(configuration.GetConnectionString("Tasks"))
            );
        }

        public static void ConfigureTokenJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration.GetSection("Token").Get<TokenConfiguration>());
        }

        public static void ConfigureMocky(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration.GetSection("Mocky").Get<MockyConfiguration>());
        }

        public static void ConfigureServices(this IServiceCollection services) 
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMockyService, MockyService>();
            services.AddScoped<IDeveloperService, DeveloperService>();
            services.AddScoped<IProjectService, ProjectService>();
        }   
        public static void ConfigureRepositories(this IServiceCollection services) 
        {
            services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IDeveloperRepository, DeveloperRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
        }
    }
}
