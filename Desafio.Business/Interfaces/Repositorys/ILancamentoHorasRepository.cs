using Desafio.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio.Business.Interfaces
{
    public interface ILancamentoHorasRepository : IRepository<LancamentoHoras>
    {

        Task<IEnumerable<LancamentoHoras>> ObterLancamentosComDesenvolvedor();

    }
}
