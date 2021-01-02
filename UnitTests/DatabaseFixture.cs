using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using TesteDotnet.Data;
using UnitTests.Utils;

namespace UnitTests
{
    public class DatabaseFixture : IDisposable
    {
        public Context Context { get; private set; }
        public Factory Factory { get; private set; }

        public DatabaseFixture()
        {
            var builder = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .ConfigureWarnings(config => config.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            Context = new Context(builder.Options);
            Factory = new Factory(Context);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
