using FluentValidation;
using luby_app.Application.Common.Interfaces;
using luby_app.Domain.Events;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace luby_app.Application.DesenvolvedorHoras.Commands.Create
{
    public class CreateDesenvolvedorHoraCommand : IRequest<int>
    {
        public DateTime Inicio { get; set; }

        public DateTime Fim { get; set; }

        public string UsuarioId { get; set; }
    }

    public class CreateTodoListCommandHandler : IRequestHandler<CreateDesenvolvedorHoraCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoListCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateDesenvolvedorHoraCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.DesenvolvedorHora();
            entity.Inicio = request.Inicio;
            entity.Fim = request.Fim;

            var desenvolvedor = _context.Desenvolvedor.Where(el => el.UsuarioId == request.UsuarioId).First();
            desenvolvedor.DesenvolvedorHora.Add(entity);

            entity.DomainEvents.Add(new WorkHourCreatedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
