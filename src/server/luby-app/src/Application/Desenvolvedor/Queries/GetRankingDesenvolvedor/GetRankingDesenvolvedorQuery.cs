using AutoMapper;
using luby_app.Application.Common.Interfaces;
using luby_app.Application.Desenvolvedor.Queries.GetDesenvolvedorWithPagination;
using luby_app.Application.Projeto.Queries.GetProjetosWithPagination;
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
        public int Dias { get; set; }
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
            List<RankingDto> result = new List<RankingDto>();
            DateTime dataInicio = DateTime.Today.AddDays(-request.Dias);
            DateTime dataFim = DateTime.Now;

            var query = _context.DesenvolvedorHora
                            .Where(el => (el.Inicio >= dataInicio && el.Inicio <= dataFim) && (el.Fim >= dataInicio && el.Fim <= dataFim))
                            .ToList()
                            .GroupBy(x => x.Desenvolvedor);

            foreach (var dev in query)
            {
                var totalHoras = dev.Select(x => x.TotalHoras()); 

                TimeSpan media = new TimeSpan(Convert.ToInt64(totalHoras.Sum(t => t.Ticks) / request.Dias));
                 
                result.Add(new RankingDto()
                {
                    MediaHoras = media.TotalHours,
                    Desenvolvedor = _mapper.Map<DesenvolvedorDto>(dev.Key),
                    Projeto = _mapper.Map<ProjetoDto>(dev.Key.Projeto)
                });
            }

            return Task.FromResult(result.OrderByDescending(el =>el.MediaHoras).ToList());
        }
    }
}
