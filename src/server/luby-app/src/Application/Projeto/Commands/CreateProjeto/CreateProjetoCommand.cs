using luby_app.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace luby_app.Application.Projeto.Commands.CreateProjeto
{
    public class CreateProjetoCommand : IRequest<int>
    {
        public string Nome { get; set; }
    }

    public class CreateTodoListCommandHandler : IRequestHandler<CreateProjetoCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoListCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateProjetoCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Projeto();

            entity.Nome = request.Nome;

            _context.Projetos.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
