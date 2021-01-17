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

        public CreateDesenvolvedorCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MaximumLength(200).WithMessage("Nome não pode ser maior que 200 caracteres.")
                .MustAsync(BeUniqueNome).WithMessage("Nome inválido! Já existe um desenvolvedor cadastrado com esse nome.");

            RuleFor(v => v.CPF)
                .NotEmpty().WithMessage("CPF é obrigatório.")
                .MustAsync(BeUniqueCpf).WithMessage("CPF inválido! Já existe um desenvolvedor cadastrado com esse CPF.");


            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .MustAsync(BeUniqueEmail).WithMessage("Email inválido! Já existe um desenvolvedor cadastrado com esse Email.");

            RuleFor(v => v.Senha)
                .NotEmpty().WithMessage("Senha do desenvolvedor é obrigatório.");

            RuleFor(v => v.ProjetoId)
                .GreaterThan(0).WithMessage("Projeto é obrigatório.");

        }
         
        public async Task<bool> BeUniqueNome(CreateDesenvolvedorCommand model, string nome, CancellationToken cancellationToken)
        {
            return await _context.Desenvolvedor 
                .AllAsync(l => l.Nome != nome);
        }

        public async Task<bool> BeUniqueCpf(CreateDesenvolvedorCommand model, string cpf, CancellationToken cancellationToken)
        {
            return await _context.Desenvolvedor 
                .AllAsync(l => l.CPF != cpf);
        }

        public async Task<bool> BeUniqueEmail(CreateDesenvolvedorCommand model, string email, CancellationToken cancellationToken)
        {
            return await _context.Desenvolvedor 
                .AllAsync(l => l.Email != email);
        }
    }
}
