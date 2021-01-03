using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tasks.Domain._Common.Dtos;
using Tasks.Domain._Common.Enums;
using Tasks.Domain.Developers.Dtos;
using Tasks.Domain.Developers.Dtos.Ranking;
using Tasks.Domain.Developers.Dtos.Works;
using Tasks.IntegrationTests._Common;
using Tasks.IntegrationTests._Common.Results;
using Tasks.UnitTests._Common.Random;
using Xunit;

namespace Tasks.IntegrationTests.Developers
{
    public class DevelopersControllerTests : BaseTest
    {
        public DevelopersControllerTests(TasksFixture fixture) : base(fixture, "developers") { }

        [Fact]
        public async void GetDeveloperByIdTest()
        {
            var developer = EntitiesFactory.NewDeveloper().Save();

            var (status, result) = await Request.GetAsync<ResultTest<DeveloperDetailDto>>(new Uri($"{Uri}/{developer.Id}"));
            
            var developerResult = result.Data;
            Assert.Equal(Status.Success, status);
            Assert.Equal(developer.Id, developerResult.Id);
            Assert.Equal(developer.Name, developerResult.Name);
            Assert.Equal(developer.Login, developerResult.Login);
            Assert.Equal(developer.CPF, developerResult.CPF);
        }

        [Fact]
        public async void ListDevelopersTest()
        {
            EntitiesFactory.NewDeveloper().Save();
            EntitiesFactory.NewDeveloper().Save();
            var query = new PaginationDto { Page = 1, Limit = 1 };
            var expectedTotal = await DbContext.Developers.CountAsync();

            var (status, result) = await Request.GetAsync<ResultTest<IEnumerable<DeveloperListDto>>>(Uri, query);

            var developerList = result.Data;
            Assert.Equal(Status.Success, status);
            Assert.NotEmpty(developerList);
            Assert.Equal(expectedTotal, result.TotalRows);
            Assert.True(developerList.Count() == query.Limit);
        }

        [Fact]
        public async void DeveloperRankingAsync()
        {
            var projectsToRemove = DbContext.Projects.ToArray();
            var developersToRemove = DbContext.Developers.Where(d => d.Id != SessionDeveloper.Id).ToArray();
            DbContext.Developers.RemoveRange(developersToRemove);
            DbContext.Projects.RemoveRange(projectsToRemove);

            var developerFirstPosition = EntitiesFactory.NewDeveloper().Save();
            var developerSecondPosition = EntitiesFactory.NewDeveloper().Save();
            var project = EntitiesFactory.NewProject(
                developerIds: new[] { developerFirstPosition.Id, developerSecondPosition.Id }
            ).Save();
            var query = new DeveloperRankingSearchDto { ProjectId = project.Id, StartTime = default };
            EntitiesFactory.NewWork(
                hours: 10,
                id: Guid.NewGuid(), 
                developerProjectId: project.DeveloperProjects
                    .First(dp => dp.DeveloperId == developerFirstPosition.Id).Id
            ).Save();
            EntitiesFactory.NewWork(
                hours: 20,
                id: Guid.NewGuid(),
                developerProjectId: project.DeveloperProjects
                    .First(dp => dp.DeveloperId == developerFirstPosition.Id).Id
            ).Save();
            EntitiesFactory.NewWork(
                hours: 12,
                id: Guid.NewGuid(),
                developerProjectId: project.DeveloperProjects
                    .First(dp => dp.DeveloperId == developerSecondPosition.Id).Id
            ).Save();

            var (status, result) = await Request.GetAsync<ResultTest<IEnumerable<DeveloperRankingListDto>>>(new Uri($"{Uri}/ranking"), query);

            var developerList = result.Data;
            Assert.Equal(Status.Success, status);
            Assert.NotEmpty(developerList);
            Assert.Equal(3, developerList.Count());
            Assert.Equal(3, result.TotalRows);

            var firstPosition = developerList.ElementAt(0);
            Assert.Equal(developerFirstPosition.Id, firstPosition.Id);
            Assert.Equal(developerFirstPosition.Name, firstPosition.Name);
            Assert.Equal(15, firstPosition.AvgHours);
            Assert.Equal(30, firstPosition.SumHours);

            var secondPosition = developerList.ElementAt(1);
            Assert.Equal(developerSecondPosition.Id, secondPosition.Id);
            Assert.Equal(developerSecondPosition.Name, secondPosition.Name);
            Assert.Equal(12, secondPosition.AvgHours);
            Assert.Equal(12, secondPosition.SumHours);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void ListWorkDeveloperAsync(bool withFilter)
        {
            var project = EntitiesFactory.NewProject(developerIds: new[] { SessionDeveloper.Id }).Save();
            var query = new DeveloperWorkSearchClientDto { Page = 1, Limit = 1, ProjectId = withFilter ? (Guid?)project.Id : null };
            EntitiesFactory.NewWork(Guid.NewGuid(), project.DeveloperProjects.Single().Id).Save();
            EntitiesFactory.NewWork(Guid.NewGuid(), project.DeveloperProjects.Single().Id).Save();

            var (status, result) = await Request.GetAsync<ResultTest<IEnumerable<DeveloperWorkListDto>>>(new Uri($"{Uri}/{SessionDeveloper.Id}/works"), query);

            var workList = result.Data;
            Assert.Equal(Status.Success, status);
            Assert.NotEmpty(workList);
            Assert.True(result.TotalRows > 0);
            Assert.True(workList.Count() == query.Limit);
            Assert.All(workList, work =>
            {
                Assert.True(work.Hours > 0);
                Assert.NotEmpty(work.Comment);
                Assert.NotNull(work.Project);
                if (withFilter)
                {
                    Assert.Equal(project.Id, work.Project.Id);
                    Assert.Equal(project.Title, work.Project.Title);
                }
            });
        }

        [Fact]
        public async void CreateDeveloperTest()
        {
            var developerDto = new DeveloperCreateDto { 
                Id = Guid.NewGuid(),
                Login = RandomHelper.RandomString(),
                Name = RandomHelper.RandomString(),
                Password = RandomHelper.RandomNumbers(),
                CPF = RandomHelper.RandomNumbers(11)
            };

            var (status, result) = await Request.PostAsync<ResultTest>(Uri, developerDto);

            var developerDb = await DbContext.Developers.FindAsync(developerDto.Id);
            Assert.Equal(Status.Success, status);
            Assert.True(result.Success);
            Assert.Equal(developerDto.Login, developerDb.Login);
            Assert.Equal(developerDto.Name, developerDb.Name);
            Assert.Equal(developerDto.CPF, developerDb.CPF);
        }

        [Fact]
        public async void UpdateDeveloperTest()
        {
            var developer = EntitiesFactory.NewDeveloper().Save();
            var developerDto = new DeveloperUpdateDto
            {
                Id = developer.Id,
                Login = RandomHelper.RandomString(),
                Name = RandomHelper.RandomString()
            };

            var (status, result) = await Request.PutAsync<ResultTest>(new Uri($"{Uri}/{developer.Id}"), developerDto);

            var developerDb = await DbContext.Developers.FindAsync(developerDto.Id);
            await DbContext.Entry(developerDb).ReloadAsync();
            Assert.Equal(Status.Success, status);
            Assert.True(result.Success);
            Assert.Equal(developerDto.Login, developerDb.Login);
            Assert.Equal(developerDto.Name, developerDb.Name);
        }

        [Fact]
        public async void DeleteDeveloperAsync()
        {
            var developer = EntitiesFactory.NewDeveloper().Save();

            var (status, result) = await Request.DeleteAsync<ResultTest>(new Uri($"{Uri}/{developer.Id}"));

            var existDeveloper = await DbContext.Developers.AnyAsync(d => d.Id == developer.Id);
            Assert.Equal(Status.Success, status);
            Assert.True(result.Success);
            Assert.False(existDeveloper);
        }
    }
}
