using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace TesteDotnet.Data
{
    public class Context : DbContext
    {
        public Context (DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Api.Models.Developer> Developer { get; set; }

        public new DbSet<Api.Models.Entry> Entry { get; set; }

        public DbSet<Api.Models.Project> Project { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Entry>().HasKey(entry => new { entry.DeveloperId, entry.ProjectId });
        }
    }
}
