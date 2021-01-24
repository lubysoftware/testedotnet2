using ControleHoras.API.ApiDbContexts;
using ControleHoras.API.BaseModels;
using ControleHoras.API.EntityModels;
using ControleHoras.API.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ControleHoras.API.Repository
{
    public class ProjetoRepository : RepositoryBase<ProjetoRepository>,
                                     IDataGetter<Projeto>,
                                     IDataSetter<Projeto>
    {
        public IList<Projeto> PagedGet(int skip, int take)
        {
            return SkipTake(new ApiDbContext().Projetos, skip, take).ToList();
        }

        public Projeto ById(int id)
        {
            return FindById(new ApiDbContext().Projetos, id).SingleOrDefault();
        }

        public Projeto Insert(Projeto obj)
        {
            return InsertOnTable(new ApiDbContext().Projetos, obj);
        }

        public bool IsNameAlreadyRegistered(string nome)
            => new ApiDbContext().Projetos.Where(x => x.Nome.Equals(nome)).Count() > 0;
    }
}
