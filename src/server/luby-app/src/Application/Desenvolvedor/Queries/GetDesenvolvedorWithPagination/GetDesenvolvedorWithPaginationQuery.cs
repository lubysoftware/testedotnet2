using AutoMapper;
using AutoMapper.QueryableExtensions;
using luby_app.Application.Common.Interfaces;
using luby_app.Application.Common.Mappings;
using luby_app.Application.Common.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace luby_app.Application.Desenvolvedor.Queries.GetDesenvolvedorWithPagination
{
    public class GetDesenvolvedorWithPaginationQuery : IRequest<PaginatedList<DesenvolvedorDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetDesenvolvedorWithPaginationQueryHandler : IRequestHandler<GetDesenvolvedorWithPaginationQuery, PaginatedList<DesenvolvedorDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDesenvolvedorWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<DesenvolvedorDto>> Handle(GetDesenvolvedorWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Desenvolvedor
                .OrderBy(x => x.Nome)
                .ProjectTo<DesenvolvedorDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
