using Desafio.Business.Interfaces;
using Desafio.Business.Models;
using Desafio.Data.Context;


namespace Desafio.Data.Repository
{
    public class ProjetoDesenvolvedorRepository : Repository<ProjetoDesenvolvedor>, IProjetoDesenvolvedorRepository
    {
        public ProjetoDesenvolvedorRepository(DesafioDbContext context) : base(context)
        {

        }


    }
}
