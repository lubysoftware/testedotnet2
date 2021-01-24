using FluentValidation;
using luby_app.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace luby_app.Application.Desenvolvedor.Commands.CreateDesenvolvedor
{
    public class CreateDesenvolvedorCommandValidator : AbstractValidator<CreateDesenvolvedorCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICpfValidationService _cpfValidationService;

        public CreateDesenvolvedorCommandValidator(IApplicationDbContext context, ICpfValidationService cpfValidationService)
        {
            _context = context;
            _cpfValidationService = cpfValidationService;

            RuleFor(v => v.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MaximumLength(200).WithMessage("Nome não pode ser maior que 200 caracteres.")
                .MustAsync(BeUniqueNome).WithMessage("Nome inválido! Já existe um desenvolvedor cadastrado com esse nome.");
             
            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido.")
                .MustAsync(BeUniqueEmail).WithMessage("Email inválido! Já existe um desenvolvedor cadastrado com esse Email.");

            RuleFor(v => v.Senha)
                .NotEmpty().WithMessage("Senha do desenvolvedor é obrigatório.")
                .MinimumLength(6).WithMessage("Senha precisa conter no mínimo 6 caracteres.");

            RuleFor(v => v.ProjetoId)
                .GreaterThan(0).WithMessage("Projeto é obrigatório.");

            RuleFor(v => v.CPF)
                .NotEmpty().WithMessage("CPF é obrigatório.")
                .MustAsync(BeUniqueCpf).WithMessage("CPF inválido! Já existe um desenvolvedor cadastrado com esse CPF.")
                .MustAsync(BeValidIntegrationCpf).WithMessage("CPF inválido!");

        }

        public async Task<bool> BeUniqueNome(CreateDesenvolvedorCommand model, string nome, CancellationToken cancellationToken)
        {
            return await _context.Desenvolvedor
                .AllAsync(l => l.Nome != nome);
        }
         
        public async Task<bool> BeUniqueEmail(CreateDesenvolvedorCommand model, string email, CancellationToken cancellationToken)
        {
            return await _context.Desenvolvedor
                .AllAsync(l => l.Email != email);
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
