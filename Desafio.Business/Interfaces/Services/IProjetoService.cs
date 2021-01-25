using Desafio.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Desafio.Business.Interfaces
{
    public interface IProjetoService : IDisposable
    {
        Task<Projeto> BuscarPorID(int id);
        Task<IEnumerable<Projeto>> BuscarTodos();
        Task<IEnumerable<Projeto>> BuscarTodosPaginado(int pagina, int registros);
        Task<Projeto> Adicionar(Projeto projeto);
        Task<ProjetoDesenvolvedor> AdicionarDesenvolvedor(int projetoID, int desenvolvedorID);
        Task<Projeto> Atualizar(Projeto projeto);
        Task Remover(int id);


    }
}