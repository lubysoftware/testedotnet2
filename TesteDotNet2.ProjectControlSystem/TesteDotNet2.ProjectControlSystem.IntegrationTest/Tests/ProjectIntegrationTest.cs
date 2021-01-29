using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;
using TesteDotNet2.ProjectControlSystem.IntegrationTest.Helper;
using TesteDotNet2.ProjectControlSystem.IntegrationTest.Services;
using TesteDotNet2.ProjectControlSystem.Services.ViewModel;
using Xunit;

namespace TesteDotNet2.ProjectControlSystem.IntegrationTest.Tests
{
    public class ProjectIntegrationTest : IClassFixture<AppTestFixture>
    {
        private const string BasePath = "https://localhost:44305/project/";
        readonly AppTestFixture fixture;
        readonly HttpClient client;

        public ProjectIntegrationTest(AppTestFixture fixture)
        {
            this.fixture = fixture;
            client = fixture.CreateClient();
        }

        [Fact]
        public async void Project_Get_Success()
        {
            var page = "1";
            var size = "10";

            // Act
            var response = await client.GetAsync(BasePath + "get?page=" + page + "&size=" + size);

            List<Project> projects = await response.Content.ReadAsAsync<List<Project>>();

            // Assert1
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.True(projects.Count > 0);
            // Assert2
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async void Project_GetById_Success()
        {
            var id = Guid.Parse("beaf506e-991f-4515-9c3d-2ace55ef1b25");

            // Act
            var response = await client.GetAsync(BasePath + "getbyid?id=" + id);

            Project project = await response.Content.ReadAsAsync<Project>();

            // Assert1
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.True(project.ProjectId == id);
            // Assert2
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async void Project_Post_Success()
        {
            var project = new Project()
            {                
                Name = "Sales"
            };


            // Act
            var response = await client.PostAsync(
            BasePath + "add",
            new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            });

            Project projectResponse = await response.Content.ReadAsAsync<Project>();

            // Assert1
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.Equal(project.ProjectId, projectResponse.ProjectId);
            // Assert2
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());

        }

        [Fact]
        public async void Project_Post_BadRequest()
        {
            var project = new Project()
            {                
            };

            // Act
            var response = await client.PostAsync(
            BasePath + "add",
            new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            });

            Project projectResponse = await response.Content.ReadAsAsync<Project>();

            // Assert1
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }       

        [Fact]
        public async void Project_Put_Success()
        {
            var project = new Project()
            {                
                Name = TestHelper.GetAleatoryName(),
                ProjectId = Guid.Parse("beaf506e-991f-4515-9c3d-2ace55ef1b25")
            };

            // Act
            var response = await client.PutAsync(
            BasePath + "update",
            new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            });

            Project projectResponse = await response.Content.ReadAsAsync<Project>();

            // Assert1
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(projectResponse.Name, project.Name);

            // Assert2
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async void Project_Put_BadRequest()
        {
            var project = new Project()
            {                
            };

            // Act
            var response = await client.PutAsync(
            BasePath + "update",
            new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            });

            Project projectResponse = await response.Content.ReadAsAsync<Project>();

            // Assert1
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void Project_Delete_Success()
        {
            string messageDelete = "Registro excluído com sucesso!";
            var project = new Project()
            {                
                Name = "Sales"
            };

            // Act
            var response = await client.PostAsync(
            BasePath + "add",
            new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            });

            Project projectResponse = await response.Content.ReadAsAsync<Project>();

            // Assert1
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseDelete = await client.DeleteAsync(BasePath + "delete/?id=" + project.ProjectId);
            ProjectViewModel projectDeleteResponse = await responseDelete.Content.ReadAsAsync<ProjectViewModel>();

            Assert.Equal(HttpStatusCode.OK, responseDelete.StatusCode);
            Assert.Equal(messageDelete, projectDeleteResponse.Messages.FirstOrDefault());


            Assert.Equal(project.ProjectId, projectResponse.ProjectId);
            // Assert2
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());

        }

        [Fact]
        public async void Project_Delete_Fail()
        {
            string messageDelete = "Não foi possível excluir o registro";
            var project = new Project()
            {                
                Name = "Sales"
            };

            var responseDelete = await client.DeleteAsync(BasePath + "delete/?id=" + project.ProjectId);
            ProjectViewModel projectDeleteResponse = await responseDelete.Content.ReadAsAsync<ProjectViewModel>();

            Assert.Equal(HttpStatusCode.BadRequest, responseDelete.StatusCode);
            Assert.Equal(messageDelete, projectDeleteResponse.Messages.FirstOrDefault());
        }
    }
}

