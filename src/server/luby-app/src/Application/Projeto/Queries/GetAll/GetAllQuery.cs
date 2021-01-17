using AutoMapper;
using AutoMapper.QueryableExtensions;
using luby_app.Application.Common.Interfaces;
using luby_app.Application.Projeto.Queries.GetProjetosWithPagination;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace luby_app.Application.Projeto.Queries.GetAll
{
    public class GetAllQuery : IRequest<IEnumerable<ProjetoDto>>
    { 
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<ProjetoDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<IEnumerable<ProjetoDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var vm = _context.Projetos
                                .OrderBy(x => x.Nome)
                                .ProjectTo<ProjetoDto>(_mapper.ConfigurationProvider)
                                .AsEnumerable();

            return Task.FromResult(vm);
        }
    }
}
