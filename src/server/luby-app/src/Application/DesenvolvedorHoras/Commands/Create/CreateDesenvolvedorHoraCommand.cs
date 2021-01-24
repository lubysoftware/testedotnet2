using FluentValidation;
using luby_app.Application.Common.Interfaces;
using luby_app.Domain.Entities;
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
            var desenvolvedor = _context.Desenvolvedor.Where(el => el.UsuarioId == request.UsuarioId).First();

            var entity = new DesenvolvedorHora()
            {
                Inicio = request.Inicio,
                Fim = request.Fim,
                ProjetoId = desenvolvedor.Projeto.Id,
                DesenvolvedorId = desenvolvedor.Id
            };

            entity.DomainEvents.Add(new WorkHourCreatedEvent(entity)); 

            _context.DesenvolvedorHora.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
             
            return entity.Id;
        }
    }
}
