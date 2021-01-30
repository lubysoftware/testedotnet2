using Api_Teste.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Teste.Repository
{
    public interface IDesenvolvedorRepository
    {
        void CadastrarDesenvolvedor(Desenvolvedor desenvolvedor);

        List<Desenvolvedor> ListarDesenvolvedor();

        Desenvolvedor ObterDesenvolvedor(int id);

        void ExcluirDesenvolvedor(int id);

        void AtualizarDesenvolvedor(int id,Desenvolvedor desenvolvedor);

    }
}
