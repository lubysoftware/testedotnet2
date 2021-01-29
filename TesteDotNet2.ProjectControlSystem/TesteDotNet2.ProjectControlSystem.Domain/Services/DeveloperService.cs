using System;
using System.Collections.Generic;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;
using TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Repository;
using TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Service;

namespace TesteDotNet2.ProjectControlSystem.Domain.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IDeveloperRepository developerRepository;
        public DeveloperService(IDeveloperRepository DeveloperRepository)
        {
            this.developerRepository = DeveloperRepository;
        }
        public Developer Add(Developer developer)
        {
            var response = developerRepository.GetByCPF(developer.CPF);
            if (response != null)
            {
                response.Messages = new List<string>();
                response.Messages.Add("Registro já existe");
                return response;
            }

            var responseValidateCPF = this.developerRepository.ValidateCPFAsync(developer.CPF);
            if (responseValidateCPF.Result.message != "Autorizado")
            {
                var responseInvalid = new Developer()
                {
                    Messages = new List<string>()                   
                };
                responseInvalid.Messages.Add("Registro inválido");
                return responseInvalid;
            }

            return this.developerRepository.Add(developer);
        }

        public bool Delete(Guid id)
        {
            return this.developerRepository.Delete(id);
        }

        public List<Developer> Get(int page, int size)
        {
            return this.developerRepository.Get(page, size);
        }

        public Developer GetById(Guid id)
        {
            return this.developerRepository.GetById(id);
        }

        public List<ReportDeveloperResponse> GetRankingOfHoursWorked()
        {
            return developerRepository.GetRankingOfHoursWorked();
        }

        public Developer Update(Developer developer)
        {
            return this.developerRepository.Update(developer);
        }
    }
}
