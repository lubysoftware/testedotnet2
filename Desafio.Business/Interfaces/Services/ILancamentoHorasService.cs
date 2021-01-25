using Desafio.Business.DTO;
using Desafio.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Desafio.Business.Interfaces
{
    public interface ILancamentoHorasService : IDisposable
    {
        Task<LancamentoHoras> BuscarPorID(int id);
        Task<IEnumerable<LancamentoHoras>> BuscarTodos();
        Task<LancamentoHoras> Adicionar(LancamentoHoras lancamento);
        Task<LancamentoHoras> Atualizar(LancamentoHoras lancamento);
        Task Remover(int id);

        Task<IEnumerable<RanqueamentoDTO>> RanqueamentoDaSemana();
    }
}