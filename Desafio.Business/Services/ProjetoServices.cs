using Desafio.Business.Interfaces;
using Desafio.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Desafio.Business.Services
{

    public class ProjetoService : IProjetoService
    {

        private readonly IProjetoRepository _projetoRepository;
        private readonly IDesenvolvedorRepository _desenvolvedorRepository;
        private readonly IProjetoDesenvolvedorRepository _projetoDesenvolvedorRepository;

        public ProjetoService(IProjetoRepository projetoRepository, IProjetoDesenvolvedorRepository projetoDesenvolvedorRepository, IDesenvolvedorRepository desenvolvedorRepository)
        {
            _projetoRepository = projetoRepository;
            _projetoDesenvolvedorRepository = projetoDesenvolvedorRepository;
            _desenvolvedorRepository = desenvolvedorRepository;
        }

        public async Task<Projeto> Adicionar(Projeto projeto)
        {

            await _projetoRepository.Adicionar(projeto);
            return projeto;
        }

        public async Task<Projeto> Atualizar(Projeto projeto)
        {

            await _projetoRepository.Atualizar(projeto);
            return projeto;
        }

        public async Task<Projeto> BuscarPorID(int id)
        {
            return await _projetoRepository.ObterPorId(id);
        }

        public async Task<IEnumerable<Projeto>> BuscarTodos()
        {
            return await _projetoRepository.ObterTodos();
        }

        public async Task<IEnumerable<Projeto>> BuscarTodosPaginado(int pagina, int registros)
        {
            return await _projetoRepository.ObterTodosPaginados(pagina, registros);
        }


        public async Task<ProjetoDesenvolvedor> AdicionarDesenvolvedor(int projetoID, int desenvolvedorID)
        {

            var desenvolvedor = await _desenvolvedorRepository.ObterPorId(desenvolvedorID);
            if (desenvolvedor == null)
            {
                throw new Exception("Desenvolvedor não Cadastrado");
            }

            var projeto = await _projetoRepository.ObterPorId(projetoID);
            if (projeto == null)
            {
                throw new Exception("Projeto não Cadastrado");
            }

            var projetoDesenvolvedor = new ProjetoDesenvolvedor
            {
                DesenvolvedorID = desenvolvedorID,
                ProjetoID = projetoID
            };

            await _projetoDesenvolvedorRepository.Adicionar(projetoDesenvolvedor);

            return projetoDesenvolvedor;
        }

        public async Task Remover(int id)
        {
            var projeto = await _projetoRepository.ObterPorId(id);

            await _projetoRepository.Remover(projeto);
        }

        public void Dispose()
        {
            _projetoRepository.Dispose();
            _projetoDesenvolvedorRepository.Dispose();
        }


    }


}