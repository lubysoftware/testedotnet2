using luby_app.Domain.Common;
using System.Threading.Tasks;

namespace luby_app.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
