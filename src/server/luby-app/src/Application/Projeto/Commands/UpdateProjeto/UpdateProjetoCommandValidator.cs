using luby_app.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace luby_app.Application.Projeto.Commands.UpdateProjeto
{
    public class UpdateProjetoCommandValidator : AbstractValidator<UpdateProjetoCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProjetoCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MaximumLength(200).WithMessage("Nome não pode ser maior que 200 caracteres.")
                .MustAsync(BeUniqueTitle).WithMessage("Nome de Projeto já existe.");
        }

        public async Task<bool> BeUniqueTitle(UpdateProjetoCommand model, string nome, CancellationToken cancellationToken)
        {
            return await _context.Projetos
                .Where(l => l.Id != model.Id)
                .AllAsync(l => l.Nome != nome);
        }
    }
}
