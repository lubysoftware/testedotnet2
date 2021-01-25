using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Repositories.Domain; 
 using Microsoft.Extensions.Logging;
using Infrastructure.Repositories.Domain.EFCore;
using Microsoft.EntityFrameworkCore.Storage;
using NUnit.Framework;
using System.Threading.Tasks;
using UnitTest.Integration.Repositories.DBConfiguration.EFCore;
using UnitTest.Integration.Repositories.Repositories.DataBuilder;
using System.Linq;
using System.Collections.Generic;
using Domain.Entities;
using System;

namespace UnitTest.Integration.Repositories.Repositories.EntityFramework
{
    [TestFixture]
    public class DeveloperRepositoryTest
    {
        private ApplicationContext dbContext;
        private IDbContextTransaction transaction;

        private IDeveloperRepository DeveloperEntityFramework;
        private DeveloperBuilder builder;

        [OneTimeSetUp]
        public void GlobalPrepare()
        {
            dbContext = new EntityFrameworkConnection().DataBaseConfiguration();
        }

        [SetUp]
        public void Inicializa()
        {
            DeveloperEntityFramework = new DeveloperRepository(dbContext);
            builder = new DeveloperBuilder();
            transaction = dbContext.Database.BeginTransaction();
        }

        [TearDown]
        public void ExecutadoAposExecucaoDeCadaTeste()
        {
            transaction.Rollback();
        }

        [Test]
        public async Task AddAsync()
        {
            var result = await DeveloperEntityFramework.AddAsync(builder.CreateDeveloper());
            Assert.IsTrue( Guid.TryParse(result.Id.ToString(), out _));
        }

        [Test]
        public async Task AddRangeAsync()
        {
            var result = await DeveloperEntityFramework.AddRangeAsync(builder.CreateDeveloperList(3));
            Assert.AreEqual(3, result);
        }

        [Test]
        public async Task RemoveAsync()
        {
            var inserted = await DeveloperEntityFramework.AddAsync(builder.CreateDeveloper());
            var result = await DeveloperEntityFramework.RemoveAsync(inserted.Id);
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task RemoveAsyncObj()
        {
            var inserted = await DeveloperEntityFramework.AddAsync(builder.CreateDeveloper());
            var result = await DeveloperEntityFramework.RemoveAsync(inserted);
            Assert.IsTrue(result >= 1);
        }

        [Test]
        public async Task RemoveRangeAsync()
        {
            var inserted1 = await DeveloperEntityFramework.AddAsync(builder.CreateDeveloper());
            var inserted2 = await DeveloperEntityFramework.AddAsync(builder.CreateDeveloper());
            var range = new List<Developer>()
            {
                inserted1, inserted2
            };
            var result = await DeveloperEntityFramework.RemoveRangeAsync(range);
            Assert.IsTrue(result >= 1);
        }

        [Test]
        public async Task UpdateAsync()
        {
            var inserted = await DeveloperEntityFramework.AddAsync(builder.CreateDeveloper());
            inserted.UpdatedAt = DateTime.Now;
            var result = await DeveloperEntityFramework.UpdateAsync(inserted);
            Assert.IsTrue(result >= 1);
        }

        [Test]
        public async Task UpdateRangeAsync()
        {
            var inserted1 = await DeveloperEntityFramework.AddAsync(builder.CreateDeveloper());
            var inserted2 = await DeveloperEntityFramework.AddAsync(builder.CreateDeveloper());
            inserted1.UpdatedAt = DateTime.Now;
            inserted2.UpdatedAt = DateTime.Now;
            var range = new List<Developer>()
            {
                inserted1, inserted2
            };
            var result = await DeveloperEntityFramework.UpdateRangeAsync(range);
            Assert.IsTrue(result >= 1);
        }

        [Test]
        public async Task GetByIdAsync()
        {
            var item1 = await DeveloperEntityFramework.AddAsync(builder.CreateDeveloper());
            var result = await DeveloperEntityFramework.GetByIdAsync(item1.Id);
            Assert.AreEqual(result.Id, item1.Id);
        }

        [Test]
        public async Task GetAllAsync()
        {
            var item1 = await DeveloperEntityFramework.AddAsync(builder.CreateDeveloper());
            var item2 = await DeveloperEntityFramework.AddAsync(builder.CreateDeveloper());
            var result = await DeveloperEntityFramework.GetAllAsync();
            Assert.AreEqual(result.OrderBy(u => u.Id).Where(u => u.Id == item1.Id).FirstOrDefault().Id, item1.Id);
            Assert.AreEqual(result.OrderBy(u => u.Id).Where(u => u.Id == item2.Id).FirstOrDefault().Id, item2.Id);
        }
    }
}
