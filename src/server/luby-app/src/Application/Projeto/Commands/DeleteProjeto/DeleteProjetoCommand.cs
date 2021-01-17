using luby_app.Application.Common.Exceptions;
using luby_app.Application.Common.Interfaces;
using luby_app.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace luby_app.Application.Projeto.Commands.DeleteProjeto
{
    public class DeleteProjetoCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteProjetoCommandHandler : IRequestHandler<DeleteProjetoCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteProjetoCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProjetoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Projetos
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Projeto), request.Id);
            }

            _context.Projetos.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
