using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Tasks.Ifrastructure.Contexts
{
    public class ContextFactory : IDesignTimeDbContextFactory<TasksContext>
    {
        private readonly IConfiguration _configuration;

        public ContextFactory()
        {
            _configuration = GetConfiguration();
        }

        public TasksContext CreateDbContext(string[] args)
        {
            var connectionString = _configuration.GetConnectionString("Tasks");
            var builder = new DbContextOptionsBuilder<TasksContext>();
            builder.UseMySql(connectionString);
            return new TasksContext(builder.Options);
        }

        private IConfiguration GetConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Tasks.API"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();
        }
    }
}
