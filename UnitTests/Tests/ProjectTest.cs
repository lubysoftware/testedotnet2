using Api.Models;
using Microsoft.EntityFrameworkCore;
using UnitTests.Utils;
using Xunit;

namespace UnitTests.Tests
{
    public class ProjectTests : BaseTest, IClassFixture<DatabaseFixture>
    {
        public ProjectTests(DatabaseFixture fixture) : base(fixture) { }

        [Fact]
        public async void CreateDeveloper()
        {
            var project = new Project()
            {
                Name = new Bogus.DataSets.Name().JobTitle(),
            };
            var result = await Context.Project.AddAsync(project);
            Assert.Equal(EntityState.Added, result.State);
        }

        [Fact]
        public void DeleteDeveloper()
        {
            var project = new Project()
            {
                Name = new Bogus.DataSets.Name().JobTitle(),
            };
            var result = Context.Project.Remove(project);
            Assert.Equal(EntityState.Deleted, result.State);
        }

        [Fact]
        public async void UpdateDeveloper()
        {
            var project = new Project()
            {
                Name = new Bogus.DataSets.Name().JobTitle(),
            };
            var result = await Context.Project.AddAsync(project);

            Assert.Equal(EntityState.Added, result.State);

            project.Name = new Bogus.DataSets.Name().FullName();

            result = Context.Project.Update(project);

            Assert.Equal(EntityState.Modified, result.State);
        }
    }
}