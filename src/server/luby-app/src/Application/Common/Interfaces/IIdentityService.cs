﻿using luby_app.Application.Common.Models;
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
    }
}
