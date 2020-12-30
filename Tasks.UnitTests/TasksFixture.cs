using Microsoft.EntityFrameworkCore;
using System;
using Tasks.Domain;
using Tasks.Ifrastructure.Contexts;

namespace Tasks.UnitTests
{
    public class TasksFixture : IDisposable
    {
        public readonly TasksContext DbContext;

        public TasksFixture()
        {
            var builder = new DbContextOptionsBuilder<TasksContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            DbContext = new TasksContext(builder.Options);

            TasksStartup.Configure();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
