using System;
using System.Collections.Generic;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;

namespace TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Service
{
    public interface IProjectService
    {
        Project Add(Project Project);
        Project GetById(Guid id);
        List<Project> Get(int page, int size);
        Project Update(Project Project);
        bool Delete(Guid id);
    }
}
