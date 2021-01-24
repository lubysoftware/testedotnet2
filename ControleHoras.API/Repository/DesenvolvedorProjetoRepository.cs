using ControleHoras.API.ApiDbContexts;
using ControleHoras.API.BaseModels;
using ControleHoras.API.EntityModels;
using System.Collections.Generic;
using System.Linq;

namespace ControleHoras.API.Repository
{
    public class DesenvolvedorProjetoRepository : RepositoryBase<DesenvolvedorProjetoRepository>
    {
        public IList<DesenvolvedorProjeto> FilterByIdDesenvolvedorIdProjeto(int idDesenvolvedor, int idProjeto)
        {
            return new ApiDbContext().DesenvolvedorProjeto.Where(x => x.IdProjeto == idProjeto && x.IdDesenvolvedor == idDesenvolvedor).ToList();
        }
    }
}
