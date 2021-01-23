using luby_app.Application.Common.Interfaces;
using luby_app.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace luby_app.Infrastructure.Identity
{
    public class ApplicationSettings
    {
        public string JWT_Secret { get; set; }
        public string Client_URL { get; set; }
    }

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly ApplicationSettings _appSettings;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService,
            IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _appSettings = appSettings.Value;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<(string UserName, string Email, string Role)> GetUserProfileAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            var role = await _userManager.GetRolesAsync(user);

            return (user.UserName, user.Email, role.First());
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password, string role = null)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!string.IsNullOrEmpty(role))
                await _userManager.AddToRolesAsync(user, new[] { role });

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }

        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public async Task<(bool succeeded, string token)> GetToken(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var role = await _userManager.GetRolesAsync(user);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return (true, token);
            }

            return (false, null);
        }
    } 
}
