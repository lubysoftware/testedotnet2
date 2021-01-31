using Apihorasdesenvolvedor.Dados.Contexto;
using Apihorasdesenvolvedor.Dados.Repositorio;
using Apihorasdesenvolvedor.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Apihorasdesenvolvedor.CamadaPareada.InjecaodeDependencia
{
    public class ConfiguracaoRepositorio
    {
        public static void ConfiguracaoRepositorioDependencia(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepositorio<>), typeof(BaseRepositorio<>));

            serviceCollection.AddDbContext<MyContext>(
              options => options.UseMySql("Server=testeluby.mysql.uhserver.com;Port=3306;Database=testeluby;Uid=testeluby;Pwd=Mudar@12")
            );
        }
    }
}
