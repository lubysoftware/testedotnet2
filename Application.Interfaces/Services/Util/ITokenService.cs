using System.IdentityModel.Tokens.Jwt;
using Domain.Entities;
using DTO.Request;
using DTO.Response;

namespace Application.Interfaces.Services.Util
{
    public interface ITokenService
    {
        string GenerateToken(Developer developer);
        bool ValidateToken(string authToken);
        JwtSecurityToken DecodeToken(string authToken);
    }
}
