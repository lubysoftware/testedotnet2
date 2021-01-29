using System;
using System.Collections.Generic;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;

namespace TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Service
{
    public interface ITimeSheetService
    {
        TimeSheet Add(TimeSheet timeSheet);
        TimeSheet GetById(Guid id);
        List<TimeSheet> Get(int page, int size);
        TimeSheet Update(TimeSheet timeSheet);
        bool Delete(Guid id);
    }
}
