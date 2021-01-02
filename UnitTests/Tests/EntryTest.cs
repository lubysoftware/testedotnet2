using Api.Models;
using Microsoft.EntityFrameworkCore;
using UnitTests.Utils;
using Xunit;

namespace UnitTests.Tests
{
    public class EntryTests : BaseTest, IClassFixture<DatabaseFixture>
    {
        public EntryTests(DatabaseFixture fixture) : base(fixture) { }

        [Fact]
        public async void CreateEntry()
        {
            var developer = await Factory.CreateDeveloper();
            var project = await Factory.CreateProject();
            var entry = new Entry()
            {
                InitialDate = new Bogus.DataSets.Date().Recent(),
                EndDate = new Bogus.DataSets.Date().Soon(),
                ProjectId = project.Id,
                DeveloperId = developer.Id
            };

            var result = await Context.Entry.AddAsync(entry);
            Assert.Equal(EntityState.Added, result.State);
        }

        [Fact]
        public async void DeleteEntry()
        {
            var developer = await Factory.CreateDeveloper();
            var project = await Factory.CreateProject();
            var entry = new Entry()
            {
                InitialDate = new Bogus.DataSets.Date().Recent(),
                EndDate = new Bogus.DataSets.Date().Soon(),
                ProjectId = project.Id,
                DeveloperId = developer.Id
            };

            var result = Context.Entry.Remove(entry);
            Assert.Equal(EntityState.Deleted, result.State);
        }
    }
}
