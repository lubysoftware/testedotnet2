using ControleHoras.API.ApiDbContexts;
using ControleHoras.API.BaseModels;
using ControleHoras.API.EntityModels;
using ControleHoras.API.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ControleHoras.API.Repository
{
    public class DesenvolvedorRepository : RepositoryBase<DesenvolvedorRepository>,
                                           IDataGetter<Desenvolvedor>,
                                           IDataSetter<Desenvolvedor>
    {
        public Desenvolvedor Insert(Desenvolvedor obj)
        {
            return InsertOnTable(new ApiDbContext().Desenvolvedores, obj);
        }
        public IList<Desenvolvedor> PagedGet(int skip, int take)
        {
            var result = SkipTake(new ApiDbContext().Desenvolvedores, skip, take).ToList();
            return result;
        }
        public Desenvolvedor ById(int id)
        {
            var result = FindById(new ApiDbContext().Desenvolvedores, id).SingleOrDefault();
            return result;
        }
        public bool IsCPFAlreadyRegistered(string cpf)
        {
            return new ApiDbContext().Desenvolvedores.Where(x => x.CPF.Equals(cpf)).Count() > 0;
        }
    }
}