using AutoMapper;
using luby_app.Application.Common.Interfaces;
using luby_app.Application.Desenvolvedor.Queries.GetDesenvolvedorWithPagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace luby_app.Application.Desenvolvedor.Queries.GetRankingDesenvolvedor
{
    public class GetRankingDesenvolvedorQuery : IRequest<List<RankingDto>>
    {
    }

    public class GetDesenvolvedorRankingQueryHandler : IRequestHandler<GetRankingDesenvolvedorQuery, List<RankingDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetDesenvolvedorRankingQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RankingDto>> Handle(GetRankingDesenvolvedorQuery request, CancellationToken cancellationToken)
        {
            int dias = 7;
            DateTime dataInicio = DateTime.Now.AddDays(-dias);
            DateTime dataFim = DateTime.Now.Date;

            var result = _context.DesenvolvedorHora
                                    .Where(el => (el.Inicio >= dataInicio && el.Inicio <= dataFim) && (el.Fim >= dataInicio && el.Fim <= dataFim))
                                    .GroupBy(x => x.Desenvolvedor)
                                    .Select(g => new RankingDto(g.Sum(x => x.TotalHoras) / dias, new DesenvolvedorDto()
                                    {
                                        Id = g.Key.Id,
                                        Nome = g.Key.Nome,
                                        CPF = g.Key.CPF,
                                        Email = g.Key.Email,
                                        ProjetoId = g.Key.ProjetoId
                                    }))
                                    .OrderBy(o => o.MediaHoras)
                                    .Take(5)
                                    .ToListAsync();

            var teste = result.Result;

            return await result;
        }
    }
}
