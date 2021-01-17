using AutoMapper;
using AutoMapper.QueryableExtensions;
using luby_app.Application.Common.Interfaces;
using luby_app.Application.Common.Mappings;
using luby_app.Application.Common.Models; 
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace luby_app.Application.Projeto.Queries.GetProjetosWithPagination
{
    public class GetProjetoWithPaginationQuery : IRequest<PaginatedList<ProjetoDto>>
    { 
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetProjetoWithPaginationQueryHandler : IRequestHandler<GetProjetoWithPaginationQuery, PaginatedList<ProjetoDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProjetoWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ProjetoDto>> Handle(GetProjetoWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Projetos 
                .OrderBy(x => x.Nome)
                .ProjectTo<ProjetoDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
