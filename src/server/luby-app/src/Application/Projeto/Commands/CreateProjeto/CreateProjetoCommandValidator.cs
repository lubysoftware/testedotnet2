using luby_app.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace luby_app.Application.Projeto.Commands.CreateProjeto
{
    public class CreateProjetoCommandValidator : AbstractValidator<CreateProjetoCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateProjetoCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MaximumLength(200).WithMessage("Nome não pode ser maior que 200 caracteres.")
                .MustAsync(BeUniqueTitle).WithMessage("Nome de Projeto já existe.");
        }

        public async Task<bool> BeUniqueTitle(string nome, CancellationToken cancellationToken)
        {
            return await _context.Projetos
                .AllAsync(l => l.Nome != nome);
        }
    }
}
