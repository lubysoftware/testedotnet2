using Desafio.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Desafio.Business.Interfaces
{
    public interface IDesenvolvedorService : IDisposable
    {
        Task<Desenvolvedor> BuscarPorID(int id);
        Task<IEnumerable<Desenvolvedor>> BuscarTodos();
        Task<IEnumerable<Desenvolvedor>> BuscarTodosPaginado(int pagina, int registros);
        Task<Desenvolvedor> Adicionar(Desenvolvedor desenvolvedor);
        Task<Desenvolvedor> Atualizar(Desenvolvedor desenvolvedor);
        Task Remover(int id);


    }
}