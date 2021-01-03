using Luby.Domain.Models;
using Luby.Domain.Interfaces;
using Luby.Infra.Context;
using Luby.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Luby.Application.DI
{
    public class Initializer
    {
        public Initializer(IServiceCollection service, string connection)
        { }
        public static void Configure(IServiceCollection services, string conection)
        {
            services.AddDbContext<LubyContext> (options => options.UseMySql(conection, Microsoft.EntityFrameworkCore.ServerVersion.FromString("10.1.37-mariadb")));

            services.AddScoped(typeof(IRepository<Luby.Domain.Models.Desenvolvedor>), typeof(DesenvolvedorRepository));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(DesenvolvedorService));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        }
    }
}