using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apihorasdesenvolvedor.CamadaPareada.InjecaodeDependencia;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Aplicacao
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            ConfiguracaoServico.ConfiguracaoDependenciaServico(services);
            ConfiguracaoRepositorio.ConfiguracaoRepositorioDependencia(services);
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "API HORAS DESENVOLVEDOR",
                        Version = "v1",
                        Description = "API REST criada com o ASP.NET Core para Controle Horas Devs" +
                                       "Em Desenvolvimento! Devido ao trabalho atual, não tive tempo para implantar:" +
                                       " - A camada BO no services. GetFiveTop()" +
                                       " - Não implantei o Jwt. " +
                                       " - Não implantei tambem o apontamento dos serviços externo para validação" +
                                       "......a aplicação esta incompleta mas possui arquitetura rica destinada ao mercado!!!",

                        Contact = new OpenApiContact
                        {
                            Name = "Guilherme Felix Silva",
                            Email = "desenvolvedor.guilherme@gmail.com",
                            Url = new Uri("https://www.linkedin.com/in/guilhermefdsilva/")
                        }
                    });
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Horas Desenvolvedor Luby");
                x.RoutePrefix = string.Empty;
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
