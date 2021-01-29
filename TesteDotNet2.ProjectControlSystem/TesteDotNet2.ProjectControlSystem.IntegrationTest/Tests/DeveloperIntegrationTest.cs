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
    public class DeveloperIntegrationTest : IClassFixture<AppTestFixture>
    {
        private const string BasePath = "https://localhost:44305/developer/";
        readonly AppTestFixture fixture;
        readonly HttpClient client;

        public DeveloperIntegrationTest(AppTestFixture fixture)
        {
            this.fixture = fixture;
            client = fixture.CreateClient();
        }

        [Fact]
        public async void Developer_Get_Success()
        {
            var page = "1";
            var size = "10";

            // Act
            var response = await client.GetAsync(BasePath + "get?page=" + page + "&size=" + size);

            List<Developer> developers = await response.Content.ReadAsAsync<List<Developer>>();

            // Assert1
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.True(developers.Count > 0);
            // Assert2
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async void Developer_GetReportDeveloper_Success()
        {
            // Act
            var response = await client.GetAsync(BasePath + "getReportDeveloper");

            List<ReportDeveloperResponse> developers = await response.Content.ReadAsAsync<List<ReportDeveloperResponse>>();

            // Assert1
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.True(developers.Count > 0);
            // Assert2
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async void Developer_GetById_Success()
        {
            var id = Guid.Parse("94b6955e-71d1-4409-bcb2-8f8b14c6ef09");

            // Act
            var response = await client.GetAsync(BasePath + "getbyid?id=" + id);

            Developer developer = await response.Content.ReadAsAsync<Developer>();

            // Assert1
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.True(developer.DeveloperId == id);
            // Assert2
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async void Developer_Post_Success()
        {
            var developer = new Developer() { 
                CPF = TestHelper.GetAleatoryCPF(),
                Name = "Josh"                
            };


            // Act
            var response = await client.PostAsync(
            BasePath + "add",
            new StringContent(JsonConvert.SerializeObject(developer), Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            });

            Developer developerResponse = await response.Content.ReadAsAsync<Developer>();

            // Assert1
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);           

            Assert.Equal(developer.DeveloperId, developerResponse.DeveloperId);
            // Assert2
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
            
        }

        [Fact]
        public async void Developer_Post_BadRequest()
        {
            var developer = new Developer()
            {
                CPF = TestHelper.GetAleatoryCPF()
            };

            // Act
            var response = await client.PostAsync(
            BasePath + "add",
            new StringContent(JsonConvert.SerializeObject(developer), Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            });

            Developer developerResponse = await response.Content.ReadAsAsync<Developer>();

            // Assert1
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void Developer_Post_Duplicity()
        {
            string messageDelete = "Registro já existe";
            var developer = new Developer()
            {
                CPF = "46321038539",
                DeveloperId = Guid.Parse("94b6955e-71d1-4409-bcb2-8f8b14c6ef09"),
                Name = "Maria"
            };

            // Act
            var response = await client.PostAsync(
            BasePath + "add",
            new StringContent(JsonConvert.SerializeObject(developer), Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            });

            Developer developerResponse = await response.Content.ReadAsAsync<Developer>();

            // Assert1
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(messageDelete, developerResponse.Messages.FirstOrDefault());
        }

        [Fact]
        public async void Developer_Put_Success()
        {
            var developer = new Developer()
            {
                CPF = TestHelper.GetAleatoryCPF(),
                Name = TestHelper.GetAleatoryName(),
                DeveloperId = Guid.Parse("94b6955e-71d1-4409-bcb2-8f8b14c6ef09")
            };

            // Act
            var response = await client.PutAsync(
            BasePath + "update",
            new StringContent(JsonConvert.SerializeObject(developer), Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            });

            Developer developerResponse = await response.Content.ReadAsAsync<Developer>();

            // Assert1
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(developerResponse.Name, developer.Name);

            // Assert2
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async void Developer_Put_BadRequest()
        {
            var developer = new Developer()
            {
                Name = "Sharon"
            };

            // Act
            var response = await client.PutAsync(
            BasePath + "update",
            new StringContent(JsonConvert.SerializeObject(developer), Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            });

            Developer developerResponse = await response.Content.ReadAsAsync<Developer>();

            // Assert1
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void Developer_Delete_Success()
        {
            string messageDelete = "Registro excluído com sucesso!";
            var developer = new Developer()
            {
                CPF = TestHelper.GetAleatoryCPF(),
                Name = "Josh"
            };

            // Act
            var response = await client.PostAsync(
            BasePath + "add",
            new StringContent(JsonConvert.SerializeObject(developer), Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            });

            Developer developerResponse = await response.Content.ReadAsAsync<Developer>();

            // Assert1
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseDelete = await client.DeleteAsync(BasePath + "delete/?id=" + developer.DeveloperId);
            DeveloperViewModel developerDeleteResponse = await responseDelete.Content.ReadAsAsync<DeveloperViewModel>();

            Assert.Equal(HttpStatusCode.OK, responseDelete.StatusCode);
            Assert.Equal(messageDelete, developerDeleteResponse.Messages.FirstOrDefault());


            Assert.Equal(developer.DeveloperId, developerResponse.DeveloperId);
            // Assert2
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());

        }

        [Fact]
        public async void Developer_Delete_Fail()
        {
            string messageDelete = "Não foi possível excluir o registro";
            var developer = new Developer()
            {
                CPF = "46321030000",
                Name = "Josh"
            };

            var responseDelete = await client.DeleteAsync(BasePath + "delete/?id=" + developer.DeveloperId);
            DeveloperViewModel developerDeleteResponse = await responseDelete.Content.ReadAsAsync<DeveloperViewModel>();

            Assert.Equal(HttpStatusCode.OK, responseDelete.StatusCode);
            Assert.Equal(messageDelete, developerDeleteResponse.Messages.FirstOrDefault());
        }       
    }
}

