using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Tasks.Domain._Common.Enums;
using Tasks.Domain._Common.Results;
using Tasks.Ifrastructure.Contexts;
using Tasks.Ifrastructure.Extensions;

namespace Tasks.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDatabases(_configuration);
            services.ConfigureTokenJwt(_configuration);
            services.ConfigureRepositories();
            services.ConfigureServices();

            services.AddMvcCore()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                })
                .ConfigureApiBehaviorOptions(
                    options => options.InvalidModelStateResponseFactory = ctx => {
                        var errors = ctx.ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage);
                        var result = new Result(Status.Invalid, errors);
                        return new BadRequestObjectResult(result);
                    }
                );

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.ConfigureApplication(_configuration);

            app.MigrateDatabase<TasksContext>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
