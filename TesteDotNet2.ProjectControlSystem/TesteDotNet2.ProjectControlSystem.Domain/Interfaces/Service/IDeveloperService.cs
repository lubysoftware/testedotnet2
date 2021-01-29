using System;
using System.Collections.Generic;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;

namespace TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Service
{
    public interface IDeveloperService
    {
        Developer Add(Developer developer);
        Developer GetById(Guid id);
        List<Developer> Get(int page, int size);
        Developer Update(Developer developer);
        bool Delete(Guid id);
        List<ReportDeveloperResponse> GetRankingOfHoursWorked();
    }
}

