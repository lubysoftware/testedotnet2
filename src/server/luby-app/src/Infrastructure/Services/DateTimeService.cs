using luby_app.Application.Common.Interfaces;
using System;

namespace luby_app.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
