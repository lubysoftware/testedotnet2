using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Apihorasdesenvolvedor.Dados.Contexto
{
    public class CriandoContext : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            var strconnection = "Server=testeluby.mysql.uhserver.com;Port=3306;Database=testeluby;Uid=testeluby;Pwd=Mudar@12";
            var optionBuilder = new DbContextOptionsBuilder<MyContext>();
            optionBuilder.UseMySql(strconnection);
            return new MyContext(optionBuilder.Options);
        }
    }
}
