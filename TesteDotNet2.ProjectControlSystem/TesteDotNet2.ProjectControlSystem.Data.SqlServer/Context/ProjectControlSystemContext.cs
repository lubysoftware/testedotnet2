using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using TesteDotNet2.ProjectControlSystem.Data.SqlServer.Extension;
using TesteDotNet2.ProjectControlSystem.Data.SqlServer.Mappings;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;

namespace TesteDotNet2.ProjectControlSystem.Data.SqlServer.Context
{
    public class ProjectControlSystemContext : DbContext
    {
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.AddConfiguration(new DeveloperMapping());
            modelBuilder.AddConfiguration(new ProjectMapping());
            modelBuilder.AddConfiguration(new TimeSheetMapping());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var directory = System.IO.Directory.GetCurrentDirectory();

            if (directory.Contains("IntegrationTest"))
            {
                directory = directory.Substring(0, directory.Length - 39) + "Services";
            }

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection").Replace("%CONTENTROOTPATH%", directory));
        }
    }
}

