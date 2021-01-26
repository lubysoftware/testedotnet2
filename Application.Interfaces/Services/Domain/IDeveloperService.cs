using Application.Interfaces.Services.Standard;
using Domain.Entities;
using DTO.Request;
using DTO.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Domain
{
    public interface IDeveloperService : IServiceBase<Developer, DeveloperRequestDTO, DeveloperResponseDTO>
    {
        Task<IEnumerable<DeveloperResponseDTO>> GetTop5SpentTimeIdAsync();
        Task<Developer> Auth(string email, string password);
    }
}
