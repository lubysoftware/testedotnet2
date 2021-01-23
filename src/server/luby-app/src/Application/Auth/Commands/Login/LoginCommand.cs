using luby_app.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace luby_app.Application.Auth.Commands.Login
{
    public class LoginCommand : IRequest<(bool succeeded, string token)>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class CreateDesenvolvedorCommandHandler : IRequestHandler<LoginCommand, (bool succeeded, string token)>
    { 
        private readonly IIdentityService _identityService;

        public CreateDesenvolvedorCommandHandler(IIdentityService identityService)
        { 
            _identityService = identityService;
        }

        public async Task<(bool succeeded, string token)> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
             return await _identityService.GetToken(request.UserName, request.Password);
        }
    }
}
