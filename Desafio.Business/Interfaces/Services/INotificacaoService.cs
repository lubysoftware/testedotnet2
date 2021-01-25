using System.Threading.Tasks;

namespace Desafio.Business.Interfaces
{
    public interface INotificacaoService
    {
        Task<bool> EnviarNotificacao();
    }
}