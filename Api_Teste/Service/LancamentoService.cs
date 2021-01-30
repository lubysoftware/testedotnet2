using Api_Teste.Database;
using Api_Teste.Models;
using Api_Teste.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Teste.Service
{
    public class LancamentoService : ILancamentoRepository
    {
        private BaseContext baseContext;

        public LancamentoService()
        {
            baseContext = new BaseContext();
        }
        public void CadastrarLancamento(Lancamento lancamento)
        {
            lancamento.DataFim = DateTime.Now;
            lancamento.DataInicio = DateTime.Now;
            baseContext.Lancamento.Add(lancamento);
            baseContext.SaveChanges();

        }
    }
}
