using TesteDotNet2.ProjectControlSystem.Domain.Entities;
using TesteDotNet2.ProjectControlSystem.Domain.Entities.Authentication;

namespace TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Service
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        User GetById(int id);
    }
}
