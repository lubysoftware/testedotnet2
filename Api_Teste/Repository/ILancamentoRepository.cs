using Api_Teste.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Teste.Repository
{
    interface ILancamentoRepository
    {
        void CadastrarLancamento(Lancamento lancamento);
       
    }
}
