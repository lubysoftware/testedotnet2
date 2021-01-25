using Desafio.Business.Interfaces;
using Desafio.Business.Models;
using Desafio.Data.Context;


namespace Desafio.Data.Repository
{
    public class ProjetoRepository : Repository<Projeto>, IProjetoRepository
    {
        public ProjetoRepository(DesafioDbContext context) : base(context)
        {

        }


    }
}
