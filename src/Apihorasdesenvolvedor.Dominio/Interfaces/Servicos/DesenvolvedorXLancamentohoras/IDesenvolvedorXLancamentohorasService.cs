using System.Collections.Generic;
using System.Threading.Tasks;
using Apihorasdesenvolvedor.Dominio.Entidades;

namespace Apihorasdesenvolvedor.Dominio.Interfaces.Servicos.DesenvolvedorXLancamentohoras
{
    public interface IDesenvolvedorXLancamentohorasService
    {
        Task<DesenvolvedorXLancamentohorasEntity> Get(int id);
        Task<IEnumerable<DesenvolvedorXLancamentohorasEntity>> GetAll();
        Task<DesenvolvedorXLancamentohorasEntity> Post(DesenvolvedorXLancamentohorasEntity desenvolvedorXLancamentohoras);
        Task<DesenvolvedorXLancamentohorasEntity> Put(DesenvolvedorXLancamentohorasEntity desenvolvedorXLancamentohoras);
        Task<bool> Delete(int id);
        Task<IEnumerable<DesenvolvedorXLancamentohorasEntity>> GetFiveTop();
    }
}
