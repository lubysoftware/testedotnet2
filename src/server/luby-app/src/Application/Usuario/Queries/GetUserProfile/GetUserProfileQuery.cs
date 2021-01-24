using AutoMapper;
using luby_app.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace luby_app.Application.Usuario.Queries.GetUserProfile
{
    public class GetUserProfileQuery : IRequest<UserProfileDto>
    {
        public string UserId { get; set; }
    }

    public class GetUserProfileHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto>
    { 
        private readonly IIdentityService _identityService;

        public GetUserProfileHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
        { 
            _identityService = identityService;
        }

        public async Task<UserProfileDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        { 
            var user = await _identityService.GetUserProfileAsync(request.UserId);

            if (user.UserName == null)
                return null;

            return await Task.FromResult(new UserProfileDto() { Email = user.Email, UserName = user.UserName, Role = user.Role });
        }
    }

}
