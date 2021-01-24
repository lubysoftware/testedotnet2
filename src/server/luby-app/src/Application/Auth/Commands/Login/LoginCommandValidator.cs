using FluentValidation;

namespace luby_app.Application.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(v => v.UserName)
                .NotEmpty().WithMessage("Email inválido. Favor, informe seu email e tente novamente.");

            RuleFor(v => v.Password)
             .NotEmpty().WithMessage("Senha inválida. Favor, informe sua senha e tente novamente.");
        }
    }
}
