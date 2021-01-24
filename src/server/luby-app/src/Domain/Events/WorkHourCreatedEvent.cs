using luby_app.Domain.Common;
using luby_app.Domain.Entities;

namespace luby_app.Domain.Events
{
    public class WorkHourCreatedEvent : DomainEvent
    {
        public WorkHourCreatedEvent(DesenvolvedorHora item)
        {
            Item = item;
        }

        public DesenvolvedorHora Item { get; }
    }
}
