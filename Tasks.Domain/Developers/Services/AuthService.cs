using System;
using System.Threading.Tasks;
using Tasks.Domain._Common.Results;
using Tasks.Domain.Developers.Dtos;
using Tasks.Domain.Developers.Entities;
using Tasks.Domain.Developers.Repositories;

namespace Tasks.Domain.Developers.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDeveloperRepository _developerRepository;

        public AuthService(IDeveloperRepository developerRepository)
        {
            _developerRepository = developerRepository;
        }

        public Task<TokenDto> GenerateJwtTokenAsync(Developer developer)
        {
            throw new NotImplementedException();
        }

        public Task<Result<TokenDto>> LoginAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }
    }
}
