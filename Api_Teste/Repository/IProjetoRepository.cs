using Api_Teste.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Teste.Repository
{
    public interface IProjetoRepository
    {
        void CadastrarProjeto(Projeto projeto);

        List<Projeto> ListarProjeto();

        Projeto ObterProjeto(int id);

        void ExcluirProjeto(int id);

        void AtualizarProjeto(int id, Projeto projeto);
    }
}
