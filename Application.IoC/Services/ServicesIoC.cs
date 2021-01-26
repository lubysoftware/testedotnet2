using Application.Interfaces.Services.Domain;
using Application.Interfaces.Services.Standard;
using Application.Interfaces.Services.Util;
using Application.Services.Domain;
using Application.Services.Standard;
using Application.Services.Util;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IoC
{
    public static class ServicesIoC
    {
        public static void ApplicationServicesIoC(this IServiceCollection services)
        {
            //services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));

            services.AddScoped<IDeveloperService, DeveloperService>();
            services.AddScoped<IProjectService, ProjectService>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IHttpClientService, HttpClientService>();
        }
    }
}
