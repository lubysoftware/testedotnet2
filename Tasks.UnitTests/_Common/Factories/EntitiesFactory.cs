using System;
using Tasks.Domain.Developers.Entities;
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
            string password = default
        ) {
            var developer = new Developer(
                id: Guid.Empty,
                name: RandomHelper.RandomString(),
                login: RandomHelper.RandomString(),
                cpf: RandomHelper.RandomNumbers(11),
                password: password ?? RandomHelper.RandomString()
            );

            return new BuilderFactory<Developer>(developer, _context);
        }
    }
}
