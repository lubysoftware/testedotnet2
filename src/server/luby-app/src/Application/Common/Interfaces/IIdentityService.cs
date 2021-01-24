using luby_app.Application.Common.Models;
using System.Threading.Tasks;

namespace luby_app.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password, string role = null);

        Task<Result> DeleteUserAsync(string userId);

        Task<(bool succeeded, string token)> GetToken(string userName, string password);

        Task<(string UserName, string Email, string Role)> GetUserProfileAsync(string userId);

        Task<bool> UpdateUserEmail(string userId, string newEmail);
    }
}
