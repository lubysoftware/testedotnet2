using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tasks.Domain._Common.Dtos;
using Tasks.Domain._Common.Enums;
using Tasks.Domain.Projects.Dtos;
using Tasks.IntegrationTests._Common;
using Tasks.IntegrationTests._Common.Results;
using Tasks.UnitTests._Common.Random;
using Xunit;

namespace Tasks.IntegrationTests.Projects
{
    public class ProjectsControllerTests : BaseTest
    {
        public ProjectsControllerTests(TasksFixture fixture) : base(fixture, "/projects") { }

        [Fact]
        public async void GetProjectByIdTest()
        {
            var project = EntitiesFactory.NewProject().Save();

            var (status, result) = await Request.GetAsync<ResultTest<ProjectDetailDto>>(new Uri($"{Uri}/{project.Id}"));

            var projectResult = result.Data;
            Assert.Equal(Status.Success, status);
            Assert.Equal(project.Id, projectResult.Id);
            Assert.Equal(project.Title, projectResult.Title);
            Assert.Equal(project.Description, projectResult.Description);
        }

        [Fact]
        public async void ListProjectsTest()
        {
            var query = new PaginationDto { Page = 1, Limit = 1 };
            EntitiesFactory.NewProject().Save();
            EntitiesFactory.NewProject().Save();

            var (status, result) = await Request.GetAsync<ResultTest<IEnumerable<ProjectListDto>>>(Uri, query);

            var projectList = result.Data;
            Assert.Equal(Status.Success, status);
            Assert.NotEmpty(projectList);
            Assert.True(projectList.Count() == query.Limit);
        }

        [Fact]
        public async void CreateProjectTest()
        {
            var projectDto = new ProjectCreateDto
            {
                Id = Guid.NewGuid(),
                Title = RandomHelper.RandomString(),
                Description = RandomHelper.RandomString(490)
            };

            var (status, result) = await Request.PostAsync<ResultTest>(Uri, projectDto);

            var projectDb = await DbContext.Projects.FindAsync(projectDto.Id);
            Assert.Equal(Status.Success, status);
            Assert.True(result.Success);
            Assert.Equal(projectDto.Title, projectDb.Title);
            Assert.Equal(projectDto.Description, projectDb.Description);
        }

        [Fact]
        public async void UpdateProjectTest()
        {
            var project = EntitiesFactory.NewProject().Save();
            var projectDto = new ProjectUpdateDto
            {
                Id = project.Id,
                Title = RandomHelper.RandomString(),
                Description = RandomHelper.RandomString(490)
            };

            var (status, result) = await Request.PutAsync<ResultTest>(new Uri($"{Uri}/{project.Id}"), projectDto);

            var projectDb = await DbContext.Projects.FindAsync(projectDto.Id);
            await DbContext.Entry(projectDb).ReloadAsync();
            Assert.Equal(Status.Success, status);
            Assert.True(result.Success);
            Assert.Equal(projectDto.Title, projectDb.Title);
            Assert.Equal(projectDto.Description, projectDb.Description);
        }

        [Fact]
        public async void DeleteProjectAsync()
        {
            var project = EntitiesFactory.NewProject().Save();

            var (status, result) = await Request.DeleteAsync<ResultTest>(new Uri($"{Uri}/{project.Id}"));

            var existProject = await DbContext.Projects.AnyAsync(d => d.Id == project.Id);
            Assert.Equal(Status.Success, status);
            Assert.True(result.Success);
            Assert.False(existProject);
        }
    }
}
