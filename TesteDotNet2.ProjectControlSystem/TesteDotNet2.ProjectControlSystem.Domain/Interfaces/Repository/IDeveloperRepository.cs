using System.Collections.Generic;
using System.Threading.Tasks;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;

namespace TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Repository
{
    public interface IDeveloperRepository : IRepository<Developer>
    {
        Task<MessageResponse> ValidateCPFAsync(string cpf);

        List<ReportDeveloperResponse> GetRankingOfHoursWorked();

        Developer GetByCPF(string cpf);
    }
}
