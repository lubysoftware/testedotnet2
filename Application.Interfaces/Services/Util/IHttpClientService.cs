using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Domain.Entities;
using DTO.Request;
using DTO.Response;

namespace Application.Interfaces.Services.Util
{
    public interface IHttpClientService
    {
        Task<string> GetAsync(string RequestUri);
    }
}
