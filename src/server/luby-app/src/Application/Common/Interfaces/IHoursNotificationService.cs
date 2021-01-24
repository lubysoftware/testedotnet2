using System.Threading.Tasks;

namespace luby_app.Application.Common.Interfaces
{
    public interface IHoursNotificationService
    {
        Task<bool> Send();
    }
}
