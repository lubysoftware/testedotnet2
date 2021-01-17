using luby_app.Application.Common.Exceptions;
using luby_app.Application.Common.Interfaces;
using luby_app.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace luby_app.Application.Projeto.Commands.UpdateProjeto
{
    public class UpdateProjetoCommand : IRequest
    {
        public int Id { get; set; }

        public string Nome { get; set; }
    }

    public class UpdateProjetoCommandHandler : IRequestHandler<UpdateProjetoCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProjetoCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProjetoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Projetos.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Projeto), request.Id);
            }

            entity.Nome = request.Nome;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
