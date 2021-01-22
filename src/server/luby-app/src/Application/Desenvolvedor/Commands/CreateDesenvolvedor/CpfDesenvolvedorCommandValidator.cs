using FluentValidation;
using luby_app.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace luby_app.Application.Desenvolvedor.Commands.CreateDesenvolvedor
{
    public class CpfDesenvolvedorCommandValidator : AbstractValidator<CreateDesenvolvedorCommand>
    {
        private readonly ICpfValidationService _cpfValidationService;
        private readonly IApplicationDbContext _context;

        public CpfDesenvolvedorCommandValidator(IApplicationDbContext context, ICpfValidationService cpfValidationService)
        {
            _cpfValidationService = cpfValidationService;
            _context = context;

            RuleFor(v => v.CPF)
                .NotEmpty().WithMessage("CPF é obrigatório.")
                .MustAsync(BeUniqueCpf).WithMessage("CPF inválido! Já existe um desenvolvedor cadastrado com esse CPF.")
                .MustAsync(BeValidIntegrationCpf).WithMessage("CPF inválido!");
        }

        public async Task<bool> BeValidIntegrationCpf(CreateDesenvolvedorCommand model, string cpf, CancellationToken cancellationToken)
        {
            return await _cpfValidationService.IsValid(cpf); 
        }

        public async Task<bool> BeUniqueCpf(CreateDesenvolvedorCommand model, string cpf, CancellationToken cancellationToken)
        {
            return await _context.Desenvolvedor
                .AllAsync(l => l.CPF != cpf);
        }
    }
}
