using System.Collections.Generic;
using System.Threading.Tasks;
using Apihorasdesenvolvedor.Dominio.Entidades;

namespace Apihorasdesenvolvedor.Dominio.Interfaces.Servicos.Projeto
{
    public interface IProjetoService
    {
        Task<ProjetoEntity> Get(int id);
        Task<IEnumerable<ProjetoEntity>> GetAll();
        Task<ProjetoEntity> Post(ProjetoEntity projeto);
        Task<ProjetoEntity> Put(ProjetoEntity projeto);
        Task<bool> Delete(int id);
    }
}
