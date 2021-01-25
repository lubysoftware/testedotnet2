using Desafio.Business.DTO;
using Desafio.Business.Interfaces;
using Desafio.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Desafio.Business.Services
{

    public class LancamentoHorasService : ILancamentoHorasService
    {

        private readonly ILancamentoHorasRepository _lancamentoHorasRepository;
        private readonly IDesenvolvedorRepository _desenvolvedorRepository;
        private readonly IProjetoRepository _projetoRepository;
        private readonly IValidacaoService _validacaoService;
        private readonly INotificacaoService _notificacaoService;

        public LancamentoHorasService(ILancamentoHorasRepository lancamentoHorasRepository, IValidacaoService validacaoService, IDesenvolvedorRepository desenvolvedorRepository, IProjetoRepository projetoRepository)
        {
            _lancamentoHorasRepository = lancamentoHorasRepository;
            _validacaoService = validacaoService;
            _desenvolvedorRepository = desenvolvedorRepository;
            _projetoRepository = projetoRepository;
        }

        public async Task<LancamentoHoras> Adicionar(LancamentoHoras lancamentoHoras)
        {
            if (!_validacaoService.ValidarDesenvolvedorNoProjeto(lancamentoHoras.ProjetoId, lancamentoHoras.DesenvolvedorId))
            {
                throw new Exception("Desenvolvedor não Vinculado ao Projeto.");
            }

            await _lancamentoHorasRepository.Adicionar(lancamentoHoras);

            bool notificacaoEnviada = await _notificacaoService.EnviarNotificacao();

            return lancamentoHoras;
        }

        public async Task<LancamentoHoras> Atualizar(LancamentoHoras lancamentoHoras)
        {

            await _lancamentoHorasRepository.Atualizar(lancamentoHoras);
            return lancamentoHoras;
        }

        public async Task<LancamentoHoras> BuscarPorID(int id)
        {
            return await _lancamentoHorasRepository.ObterPorId(id);
        }

        public async Task<IEnumerable<LancamentoHoras>> BuscarTodos()
        {
            return await _lancamentoHorasRepository.ObterTodos();
        }
        
        public async Task<IEnumerable<RanqueamentoDTO>> RanqueamentoDaSemana()
        {

            DateTime dataDeHoje = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
            DateTime inicioDaSemana = dataDeHoje.AddDays(-(int)dataDeHoje.DayOfWeek);

            // Obter os lançamentos da Semana trabalhada do dia Atual
            var lancamentoDeHorasDaSemana = (await _lancamentoHorasRepository.ObterLancamentosComDesenvolvedor())
                                                                             .Where(x => x.DataInicio >= inicioDaSemana && 
                                                                                         x.DataFim <= dataDeHoje);

            // Agrupar por Desenvolvedor
            var lancamentoDeHorasPorDesenvolvedor =
                lancamentoDeHorasDaSemana
                            .GroupBy(g => new { g.DesenvolvedorId, g.Desenvolvedor.Nome })
                            // Obter Dados para ranquemento
                            .Select(x => new
                            {
                                x.Key.DesenvolvedorId,
                                x.Key.Nome,
                                DiasTrabalhados = x.GroupBy(g => g.DataInicio.Date).Count(),
                                HorasTrabalhadasEmMinutos = x.Sum(s => (s.DataFim - s.DataInicio).TotalMinutes),
                                MediaHorasTrabalhadas = (x.Sum(s => ((s.DataFim - s.DataInicio).TotalMinutes) / 60) / x.GroupBy(g => g.DataInicio.Date).Count())
                            });

            var listRanqueamento = new List<RanqueamentoDTO>();

            lancamentoDeHorasPorDesenvolvedor.OrderByDescending(x => x.MediaHorasTrabalhadas).Take(5).ToList().ForEach(ranqueado => 
            {
                listRanqueamento.Add(new RanqueamentoDTO
                {
                    DesenvolvedorID = ranqueado.DesenvolvedorId,
                    Nome = ranqueado.Nome,
                    DiasTrabalhados =ranqueado.DiasTrabalhados,
                    TotalHoras = Convert.ToDecimal(ranqueado.HorasTrabalhadasEmMinutos / 60),
                    MediaHoras = Convert.ToDecimal(ranqueado.MediaHorasTrabalhadas)
                });
            });

            return listRanqueamento;
        }



        public async Task Remover(int id)
        {
            var lancamentoHoras = await _lancamentoHorasRepository.ObterPorId(id);

            await _lancamentoHorasRepository.Remover(lancamentoHoras);
        }

        public void Dispose()
        {
            _lancamentoHorasRepository.Dispose();
        }

    }


}