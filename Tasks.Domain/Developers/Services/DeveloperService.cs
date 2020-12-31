using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Domain._Common.Dtos;
using Tasks.Domain._Common.Enums;
using Tasks.Domain._Common.Results;
using Tasks.Domain.Developers.Dtos;
using Tasks.Domain.Developers.Entities;
using Tasks.Domain.Developers.Repositories;
using Tasks.Domain.External.Services;

namespace Tasks.Domain.Developers.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IMockyService _mockyService;
        public DeveloperService(
            IDeveloperRepository developerRepository,
            IMockyService mockyService
        ) { 
            _developerRepository = developerRepository;
            _mockyService = mockyService;
        }

        public async Task<Result> CreateDeveloperAsync(DeveloperCreateDto developerDto)
        {
            var existLogin = await _developerRepository.ExistByLoginAsync(developerDto.Login);
            if (existLogin) return new Result(Status.Conflict, $"Developer with {nameof(developerDto.Login)} already exist");
            var validCpf = await _mockyService.ValidateCPF(developerDto.CPF);
            if (!validCpf.Success) return new Result(validCpf.Status, validCpf.ErrorMessages);
            if (!validCpf.Data) return new Result(Status.Invalid, $"Parameter {nameof(developerDto.CPF)} is not valid");

            var developer = new Developer(
                id: developerDto.Id,
                name: developerDto.Name,
                login: developerDto.Login,
                cpf: developerDto.CPF,
                password: developerDto.Password
            );

            await _developerRepository.CreateAsync(developer);
            return new Result();
        }

        public async Task<Result> DeleteDeveloperAsync(Guid id)
        {
            var existDeveloper = await _developerRepository.ExistAsync(id);
            if (!existDeveloper) return new Result(Status.NotFund, $"Developer with {nameof(id)} does not exist");

            var developer = await _developerRepository.GetByIdAsync(id);
            await _developerRepository.DeleteAsync(developer);
            return new Result();
        }

        public async Task<Result<DeveloperDetailDto>> GetDeveloperByIdAsync(Guid id)
        {
            var existDeveloper = await _developerRepository.ExistAsync(id);
            if (!existDeveloper) return new Result<DeveloperDetailDto>(Status.NotFund, $"Developer with {nameof(id)} does not exist");

            var developer = await _developerRepository.GetByIdAsync(id);
            var developerDetail = new DeveloperDetailDto { 
                Id = developer.Id,
                CPF = developer.CPF,
                Login = developer.Login,
                Name = developer.Name
            };

            return new Result<DeveloperDetailDto>(developerDetail);
        }

        public async Task<IEnumerable<DeveloperListDto>> ListDevelopersAsync(PaginationDto pagination)
        {
            var developersList = _developerRepository.Query()
                .Skip(pagination.Offset)
                .Take(pagination.Limit)
                .Select(d => new DeveloperListDto { 
                    Id = d.Id,
                    Name = d.Name
                })
                .ToArray();

            return await Task.FromResult(developersList);
        }

        public async Task<Result> UpdateDeveloperAsync(DeveloperUpdateDto developerDto)
        {
            var existDeveloper = await _developerRepository.ExistAsync(developerDto.Id);
            if (!existDeveloper) return new Result(Status.NotFund, $"Developer with {nameof(developerDto.Id)} does not exist");
            var existLogin = await _developerRepository.ExistByLoginAsync(developerDto.Login, developerDto.Id);
            if (existLogin) return new Result(Status.Conflict, $"Developer with {nameof(developerDto.Login)} already exist");

            var developer = await _developerRepository.GetByIdAsync(developerDto.Id);
            developer.SetData(
                name: developerDto.Name,
                login: developerDto.Login
            );

            await _developerRepository.UpdateAsync(developer);
            return new Result();
        }
    }
}
