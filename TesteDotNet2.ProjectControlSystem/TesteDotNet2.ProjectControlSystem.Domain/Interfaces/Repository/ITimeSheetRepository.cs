using System;
using System.Threading.Tasks;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;

namespace TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Repository
{
    public interface ITimeSheetRepository : IRepository<TimeSheet>
    {
        Task<MessageResponse> Notify(Guid developerId);       
    }
}
