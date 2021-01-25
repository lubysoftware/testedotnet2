using Desafio.Business.Interfaces;
using Desafio.Business.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace  Desafio.Business.Services
{

    public class DesenvolvedorService : IDesenvolvedorService
    {

        private readonly IDesenvolvedorRepository _desenvolvedorRepository;
        private readonly IValidacaoService _validacaoService;


        public DesenvolvedorService(IDesenvolvedorRepository desenvolvedorRepository, IValidacaoService validacaoService)
        {
            _desenvolvedorRepository = desenvolvedorRepository;
            _validacaoService = validacaoService;
        }

        public async Task<Desenvolvedor> Adicionar(Desenvolvedor desenvolvedor)
        {
            if (!await _validacaoService.ValidarCPF(desenvolvedor.CPF))
            {
                throw new Exception("CPF não validado");
            }

            await _desenvolvedorRepository.Adicionar(desenvolvedor);
            return desenvolvedor;
        }

        public async Task<Desenvolvedor> Atualizar(Desenvolvedor desenvolvedor)
        {

            
            await _desenvolvedorRepository.Atualizar(desenvolvedor);
            return desenvolvedor;
        }

        public async Task<Desenvolvedor> BuscarPorID(int id)
        {
           return await _desenvolvedorRepository.ObterPorId(id);
        }

        public async Task<IEnumerable<Desenvolvedor>> BuscarTodos()
        {
           return await _desenvolvedorRepository.ObterTodos();
        }

        public async Task<IEnumerable<Desenvolvedor>> BuscarTodosPaginado(int pagina, int registros)
        {
            return await _desenvolvedorRepository.ObterTodosPaginados(pagina, registros);
        }

        public async Task Remover(int id)
        {
            var desenvolvedor = await _desenvolvedorRepository.ObterPorId(id);

            await _desenvolvedorRepository.Remover(desenvolvedor);
        }

        public void Dispose()
        {
            _desenvolvedorRepository.Dispose();
        }




    }


}