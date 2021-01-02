using Api.Models;
using Brazil.Data;
using Microsoft.EntityFrameworkCore;
using UnitTests.Utils;
using Xunit;

namespace UnitTests.Tests
{
    public class DeveloperTests : BaseTest, IClassFixture<DatabaseFixture>
    {
        public DeveloperTests(DatabaseFixture fixture): base(fixture) { }

        [Fact]
        public async void CreateDeveloper()
        {
            var developer = new Developer()
            {
                Id = 1,
                Name = new Bogus.DataSets.Name().FullName(),
                CPF = _cpfGenerator.Next().ToString().RemoveNonNumeric(),
                Email = new Bogus.DataSets.Internet().Email(),
                Password = new Bogus.DataSets.Internet().Password(),
            };
            var result = await Context.Developer.AddAsync(developer);
            Assert.Equal(EntityState.Added, result.State);
        }

        [Fact]
        public void DeleteDeveloper()
        {
            var developer = new Developer()
            {
                Id = 1,
                Name = new Bogus.DataSets.Name().FullName(),
                CPF = _cpfGenerator.Next().ToString().RemoveNonNumeric(),
                Email = new Bogus.DataSets.Internet().Email(),
                Password = new Bogus.DataSets.Internet().Password(),
            };
            var result = Context.Developer.Remove(developer);
            Assert.Equal(EntityState.Deleted, result.State);
        }

        [Fact]
        public async void UpdateDeveloper()
        {
            var developer = new Developer()
            {
                Id = 1,
                Name = new Bogus.DataSets.Name().FullName(),
                CPF = _cpfGenerator.Next().ToString().RemoveNonNumeric(),
                Email = new Bogus.DataSets.Internet().Email(),
                Password = new Bogus.DataSets.Internet().Password(),
            };
            var result = await Context.Developer.AddAsync(developer);

            Assert.Equal(EntityState.Added, result.State);

            developer.Name = new Bogus.DataSets.Name().FullName();
            developer.Email = new Bogus.DataSets.Internet().Email();
            developer.CPF = _cpfGenerator.Next().ToString();
            developer.Password = new Bogus.DataSets.Internet().Password();

            result = Context.Developer.Update(developer);

            Assert.Equal(EntityState.Modified, result.State);
        }
    }
}
