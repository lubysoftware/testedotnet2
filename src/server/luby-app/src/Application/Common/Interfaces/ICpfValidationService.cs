using System.Threading.Tasks;

namespace luby_app.Application.Common.Interfaces
{
    public interface ICpfValidationService
    {
        Task<bool> IsValid(string cpf);
    }
}
