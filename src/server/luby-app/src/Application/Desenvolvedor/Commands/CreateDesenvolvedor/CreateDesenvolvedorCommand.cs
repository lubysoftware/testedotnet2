using luby_app.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace luby_app.Application.Desenvolvedor.Commands.CreateDesenvolvedor
{
    public class CreateDesenvolvedorCommand : IRequest<int>
    {
        public string Nome { get; set; }

        public string Senha { get; set; }

        public string Email { get; set; }

        public string CPF { get; set; }

        public int ProjetoId { get; set; }
    }

    public class CreateDesenvolvedorCommandHandler : IRequestHandler<CreateDesenvolvedorCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;

        public CreateDesenvolvedorCommandHandler(IApplicationDbContext context, IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        public async Task<int> Handle(CreateDesenvolvedorCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Desenvolvedor();
            entity.Nome = request.Nome;
            entity.CPF = request.CPF;
            entity.Email = request.Email;
            entity.ProjetoId = request.ProjetoId;

            var result = await _identityService.CreateUserAsync(entity.Email, request.Senha, "Desenvolvedor");
            entity.UsuarioId = result.UserId;

            _context.Desenvolvedor.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
