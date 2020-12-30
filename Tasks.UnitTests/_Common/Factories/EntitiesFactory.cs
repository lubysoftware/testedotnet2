using Tasks.Ifrastructure.Contexts;

namespace Tasks.UnitTests._Common.Factories
{
    public class EntitiesFactory
    {
        private readonly TasksContext _context;

        public EntitiesFactory(TasksContext context)
        {
            _context = context;
        }
    }
}
