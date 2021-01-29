using System;
using System.Collections.Generic;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;
using TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Repository;
using TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Service;

namespace TesteDotNet2.ProjectControlSystem.Domain.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }
        public Project Add(Project project)
        {
            return this.projectRepository.Add(project);
        }

        public bool Delete(Guid id)
        {
            return this.projectRepository.Delete(id);
        }

        public List<Project> Get(int page, int size)
        {
            return this.projectRepository.Get(page, size);
        }

        public Project GetById(Guid id)
        {
            return this.projectRepository.GetById(id);
        }

        public Project Update(Project project)
        {
            return this.projectRepository.Update(project);
        }
    }
}
