using Desafio.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Desafio.Business.Interfaces
{
    public interface IValidacaoService : IDisposable
    {

        Task<bool> ValidarCPF(string CPF);
        bool ValidarDesenvolvedorNoProjeto(int ProjetoID, int DesenvolvedorID);

    }
}