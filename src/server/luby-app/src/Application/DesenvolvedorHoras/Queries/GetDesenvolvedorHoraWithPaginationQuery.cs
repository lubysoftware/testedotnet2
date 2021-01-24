using AutoMapper;
using AutoMapper.QueryableExtensions;
using luby_app.Application.Common.Interfaces;
using luby_app.Application.Common.Mappings;
using luby_app.Application.Common.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace luby_app.Application.DesenvolvedorHoras.Queries
{
    public class GetDesenvolvedorHoraWithPaginationQuery : IRequest<PaginatedList<DesenvolvedorHoraDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string UsuarioId { get; set; }
    }

    public class GetDesenvolvedorHoraWithPaginationQueryHandler : IRequestHandler<GetDesenvolvedorHoraWithPaginationQuery, PaginatedList<DesenvolvedorHoraDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDesenvolvedorHoraWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<DesenvolvedorHoraDto>> Handle(GetDesenvolvedorHoraWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.DesenvolvedorHora.Where(el => el.Desenvolvedor.UsuarioId == request.UsuarioId)
                                                    .OrderBy(x => x.Inicio)
                                                    .ProjectTo<DesenvolvedorHoraDto>(_mapper.ConfigurationProvider)
                                                    .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
