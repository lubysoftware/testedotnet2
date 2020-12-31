using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.Tasks;
using Tasks.Domain.Developers.Entities;
using Tasks.Ifrastructure.Seeders;

namespace Tasks.Ifrastructure.Contexts
{
    public class TasksContext : DbContext
    {
        public DbSet<Developer> Developers { get; set; }

        public TasksContext(DbContextOptions<TasksContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            new TasksSeed().Seed(builder);
            base.OnModelCreating(builder);
        }
    }
}
