using System;
using Luby.Domain.Interfaces;
using System.Collections.Generic;

namespace Luby.Domain.Models
{
    public class LancamentoService
    {
        private readonly IRepository<Lancamento> _lancamentoRepository;

        public LancamentoService(IRepository<Lancamento> lancamentoRepository)
        {
            _lancamentoRepository = lancamentoRepository;
        }

        public void Save(int id,DateTime DtInicio, DateTime DtFim, int IdDesenvolvedor, int IdProjeto)
        {
            var lancamento = _lancamentoRepository.GetById(id);

            if (lancamento == null)
            {
                lancamento = new Lancamento(id, DtInicio,  DtFim,  IdDesenvolvedor, IdProjeto);
                _lancamentoRepository.Save(lancamento);
            }
            else
                lancamento.Update( DtInicio,  DtFim,  IdDesenvolvedor, IdProjeto);
        }
    }
}