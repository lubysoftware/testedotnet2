using System.Threading.Tasks;
using Tasks.Domain._Common.Results;

namespace Tasks.Domain.External.Services
{
    public interface IMockyService
    {
        Task<Result> ValidateCPF(string cpf);
        Task<Result> SendNotification(string title, string message);
    }
}
