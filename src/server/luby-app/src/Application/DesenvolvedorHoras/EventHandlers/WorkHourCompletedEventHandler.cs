using luby_app.Application.Common.Interfaces;
using luby_app.Application.Common.Models;
using luby_app.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace luby_app.Application.DesenvolvedorHoras.EventHandlers
{
    public class WorkHourCompletedEventHandler : INotificationHandler<DomainEventNotification<WorkHourCreatedEvent>>
    {
        private IHoursNotificationService _hoursNotificationService;

        public WorkHourCompletedEventHandler(IHoursNotificationService hoursNotificationService)
        {
            _hoursNotificationService = hoursNotificationService;
        }

        public async Task Handle(DomainEventNotification<WorkHourCreatedEvent> notification, CancellationToken cancellationToken)
        { 
            await _hoursNotificationService.Send(); 
        }
    }

}
