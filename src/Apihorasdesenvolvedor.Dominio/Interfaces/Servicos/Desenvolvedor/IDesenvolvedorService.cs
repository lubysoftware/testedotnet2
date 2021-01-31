using System.Collections.Generic;
using System.Threading.Tasks;
using Apihorasdesenvolvedor.Dominio.Entidades;

namespace Apihorasdesenvolvedor.Dominio.Interfaces.Servicos.Desenvolvedor
{
    public interface IDesenvolvedorService
    {
        Task<DesenvolvedorEntity> Get(int id);
        Task<IEnumerable<DesenvolvedorEntity>> GetAll();
        Task<DesenvolvedorEntity> Post(DesenvolvedorEntity desenvolvedor);
        Task<DesenvolvedorEntity> Put(DesenvolvedorEntity desenvolvedor);
        Task<bool> Delete(int id);

    }
}
