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

        public void Save(int id, string nome, string cpf, string cargo,
        string login, string senha,
        List<Desenvolvedor> lst_Desenvolvedores, List<Projeto> lst_Projetos)
        {
            var lancamento = _lancamentoRepository.GetById(id);

            if (lancamento == null)
            {
                lancamento = new Lancamento(nome, cpf, cargo, login, senha,lst_Desenvolvedores,lst_Projetos);
                _lancamentoRepository.Save(lancamento);
            }
            else
                lancamento.Update(nome, cpf, cargo, login, senha,lst_Desenvolvedores,lst_Projetos);
        }
    }
}