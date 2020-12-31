using System.Threading.Tasks;
using Tasks.Domain._Common.Results;

namespace Tasks.Domain.External.Services
{
    public interface IMockyService
    {
        Task<Result<bool>> ValidateCPF(string cpf);
        Task<Result<bool>> SendNotification(string title, string message);
    }
}
