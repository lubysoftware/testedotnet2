using System;
using Tasks.Domain.Developers.Entities;
using Tasks.Domain.Projects.Entities;
using Tasks.Ifrastructure.Contexts;
using Tasks.UnitTests._Common.Builders;
using Tasks.UnitTests._Common.Random;

namespace Tasks.UnitTests._Common.Factories
{
    public class EntitiesFactory
    {
        private readonly TasksContext _context;

        public EntitiesFactory(TasksContext context)
        {
            _context = context;
        }

        public BuilderFactory<Developer> NewDeveloper(
            Guid id = default,
            string name = default,
            string login = default,
            string password = default
        ) {
            var developer = new Developer(
                id: id == default ? Guid.Empty : id,
                name: name ?? RandomHelper.RandomString(),
                login: login ?? RandomHelper.RandomString(),
                cpf: RandomHelper.RandomNumbers(11),
                password: password ?? RandomHelper.RandomString()
            );

            return new BuilderFactory<Developer>(developer, _context);
        }

        public BuilderFactory<Project> NewProject(
            Guid id = default,
            string title = default
        )
        {
            var project = new Project(
                id: id == default ? Guid.Empty : id,
                title: title ?? RandomHelper.RandomString(),
                description: RandomHelper.RandomString(450)
            );

            return new BuilderFactory<Project>(project, _context);
        }
    }
}
