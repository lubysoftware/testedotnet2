using Desafio.Business.Interfaces;
using Desafio.Business.Models;
using Desafio.Data.Context;


namespace Desafio.Data.Repository
{
    public class DesenvolvedorRepository : Repository<Desenvolvedor>, IDesenvolvedorRepository
    {
 
        public DesenvolvedorRepository(DesafioDbContext context) : base(context)
        {
            
        }

    }
}
