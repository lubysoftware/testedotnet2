using FluentValidation;
using luby_app.Application.Common.Interfaces;
using System;

namespace luby_app.Application.DesenvolvedorHoras.Commands.Create
{
    public class CreateProjetoCommandValidator : AbstractValidator<CreateDesenvolvedorHoraCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateProjetoCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(e => e.Inicio)
                    .Must(BeAValidDate).WithMessage("Data início é obrigatório.") 
                    .LessThan(DateTime.Now).WithMessage("Data início não pode ser maior que a data atual");

            RuleFor(e => e.Fim)
                    .Must(BeAValidDate).WithMessage("Data fim é obrigatório.")
                    .GreaterThan(r => r.Inicio).WithMessage("Data início não pode ser maior que a data fim")
                    .LessThan(DateTime.Now).WithMessage("Data fim não pode ser maior que a data atual"); 
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
