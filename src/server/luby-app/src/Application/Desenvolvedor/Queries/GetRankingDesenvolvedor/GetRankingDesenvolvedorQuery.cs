using AutoMapper;
using luby_app.Application.Common.Interfaces;
using luby_app.Application.Desenvolvedor.Queries.GetDesenvolvedorWithPagination;
using MediatR;
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
        private readonly IMapper _mapper;

        public GetDesenvolvedorRankingQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<RankingDto>> Handle(GetRankingDesenvolvedorQuery request, CancellationToken cancellationToken)
        {
            DateTime dataInicio = DateTime.Today.AddDays(-7);
            DateTime dataFim = DateTime.Today;

            List<RankingDto> result = new List<RankingDto>();

            var query = _context.DesenvolvedorHora
                            .Where(el => (el.Inicio >= dataInicio && el.Inicio <= dataFim) && (el.Fim >= dataInicio && el.Fim <= dataFim))
                            .GroupBy(x => x.Desenvolvedor);

            foreach (var item in query)
            { 
                TimeSpan media = new TimeSpan(Convert.ToInt64(item.Select(x => x.TotalHoras()).Average(t => t.Ticks) / 7));
                 
                result.Add(new RankingDto(media.TotalHours, _mapper.Map<DesenvolvedorDto>(item.Key)));
            }

            return Task.FromResult(result);
        }
    }
}
