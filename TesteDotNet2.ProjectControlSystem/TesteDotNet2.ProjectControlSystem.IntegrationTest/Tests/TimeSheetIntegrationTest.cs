using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;
using TesteDotNet2.ProjectControlSystem.Domain.Entities.Authentication;
using TesteDotNet2.ProjectControlSystem.IntegrationTest.Services;
using TesteDotNet2.ProjectControlSystem.Services.ViewModel;
using Xunit;

namespace TesteDotNet2.ProjectControlSystem.IntegrationTest.Tests
{
    public class TimeSheetIntegrationTest : IClassFixture<AppTestFixture>
    {
        private const string BasePath = "https://localhost:44305/timeSheet/";
        private const string BasePathAuthentication = "https://localhost:44305/user/";
        readonly AppTestFixture fixture;
        readonly HttpClient client;

        public TimeSheetIntegrationTest(AppTestFixture fixture)
        {
            this.fixture = fixture;
            client = fixture.CreateClient();
        }

        [Fact]
        public async void TimeSheet_Get_Success()
        {
            var page = "1";
            var size = "10";

            // Act
            var response = await client.GetAsync(BasePath + "get?page=" + page + "&size=" + size);

            List<TimeSheet> timeSheets = await response.Content.ReadAsAsync<List<TimeSheet>>();

            // Assert1
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.True(timeSheets.Count > 0);
            // Assert2
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async void TimeSheet_GetById_Success()
        {
            var id = Guid.Parse("a358d10d-521e-4e3c-a1e2-077d4018087b");

            // Act
            var response = await client.GetAsync(BasePath + "getbyid?id=" + id);

            TimeSheet timeSheet = await response.Content.ReadAsAsync<TimeSheet>();

            // Assert1
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.True(timeSheet.TimeSheetId == id);
            // Assert2
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async void TimeSheet_Post_Success()
        {
            var timeSheet = new TimeSheet()
            {
                BeginDate = DateTime.Now.AddDays(-7),
                EndDate = DateTime.Now,
                ProjectId = Guid.Parse("beaf506e-991f-4515-9c3d-2ace55ef1b25"),
                DeveloperId = Guid.Parse("94b6955e-71d1-4409-bcb2-8f8b14c6ef09")
            };

            //Act
            await Autenticate();

            var response = await client.PostAsync(
            BasePath + "add",
           new StringContent(JsonConvert.SerializeObject(timeSheet), Encoding.UTF8)
           {
               Headers = { ContentType = new MediaTypeHeaderValue("application/json") }

           });

            TimeSheet timeSheetResponse = await response.Content.ReadAsAsync<TimeSheet>();

            // Assert1
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.Equal(timeSheet.TimeSheetId, timeSheetResponse.TimeSheetId);
            // Assert2
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());

        }

        [Fact]
        public async void TimeSheet_Post_Unauthorized()
        {
            var timeSheet = new TimeSheet()
            {
                BeginDate = DateTime.Now.AddDays(-7),
                EndDate = DateTime.Now,
                ProjectId = Guid.Parse("beaf506e-991f-4515-9c3d-2ace55ef1b25"),
                DeveloperId = Guid.Parse("94b6955e-71d1-4409-bcb2-8f8b14c6ef09")
            };

            //Act            

            var response = await client.PostAsync(
            BasePath + "add",
           new StringContent(JsonConvert.SerializeObject(timeSheet), Encoding.UTF8)
           {
               Headers = { ContentType = new MediaTypeHeaderValue("application/json") }

           });

            TimeSheet timeSheetResponse = await response.Content.ReadAsAsync<TimeSheet>();

            // Assert1
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void TimeSheet_Post_BadRequest()
        {
            var timeSheet = new TimeSheet()
            {
            };

            await Autenticate();

            // Act
            var response = await client.PostAsync(
            BasePath + "add",
            new StringContent(JsonConvert.SerializeObject(timeSheet), Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            });

            TimeSheet timeSheetResponse = await response.Content.ReadAsAsync<TimeSheet>();

            // Assert1
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void TimeSheet_Put_Success()
        {
            var timeSheet = new TimeSheet()
            {
                BeginDate = DateTime.Now.AddDays(-10),
                EndDate = DateTime.Now,
                ProjectId = Guid.Parse("beaf506e-991f-4515-9c3d-2ace55ef1b25"),
                DeveloperId = Guid.Parse("94b6955e-71d1-4409-bcb2-8f8b14c6ef09"),
                TimeSheetId = Guid.Parse("a358d10d-521e-4e3c-a1e2-077d4018087b")
            };

            // Act
            var response = await client.PutAsync(
            BasePath + "update",
            new StringContent(JsonConvert.SerializeObject(timeSheet), Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            });

            TimeSheet timeSheetResponse = await response.Content.ReadAsAsync<TimeSheet>();

            // Assert1
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(DateTime.Now.AddDays(-10).Day, timeSheetResponse.BeginDate.Day);


            // Assert2
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async void TimeSheet_Put_BadRequest()
        {
            var timeSheet = new TimeSheet()
            {
            };

            // Act
            var response = await client.PutAsync(
            BasePath + "update",
            new StringContent(JsonConvert.SerializeObject(timeSheet), Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            });

            TimeSheet timeSheetResponse = await response.Content.ReadAsAsync<TimeSheet>();

            // Assert1
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void TimeSheet_Delete_Success()
        {
            string messageDelete = "Registro excluído com sucesso!";
            var timeSheet = new TimeSheet()
            {
                BeginDate = DateTime.Now.AddDays(-7),
                EndDate = DateTime.Now,
                ProjectId = Guid.Parse("beaf506e-991f-4515-9c3d-2ace55ef1b25"),
                DeveloperId = Guid.Parse("94b6955e-71d1-4409-bcb2-8f8b14c6ef09")
            };

            await Autenticate();
            // Act
            var response = await client.PostAsync(
            BasePath + "add",
            new StringContent(JsonConvert.SerializeObject(timeSheet), Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            });

            TimeSheet timeSheetResponse = await response.Content.ReadAsAsync<TimeSheet>();

            // Assert1
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseDelete = await client.DeleteAsync(BasePath + "delete/?id=" + timeSheet.TimeSheetId);
            TimeSheetViewModel timeSheetDeleteResponse = await responseDelete.Content.ReadAsAsync<TimeSheetViewModel>();

            Assert.Equal(HttpStatusCode.OK, responseDelete.StatusCode);
            Assert.Equal(messageDelete, timeSheetDeleteResponse.Messages.FirstOrDefault());


            Assert.Equal(timeSheet.TimeSheetId, timeSheetResponse.TimeSheetId);
            // Assert2
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());

        }

        [Fact]
        public async void TimeSheet_Delete_Fail()
        {
            string messageDelete = "Não foi possível excluir o registro";
            var timeSheet = new TimeSheet()
            {
                
            };

            var responseDelete = await client.DeleteAsync(BasePath + "delete/?id=" + timeSheet.TimeSheetId);
            TimeSheetViewModel timeSheetDeleteResponse = await responseDelete.Content.ReadAsAsync<TimeSheetViewModel>();

            Assert.Equal(HttpStatusCode.BadRequest, responseDelete.StatusCode);
            Assert.Equal(messageDelete, timeSheetDeleteResponse.Messages.FirstOrDefault());
        }

        private async Task Autenticate()
        {
            AuthenticateRequest authenticateRequest = new AuthenticateRequest()
            {
                Username = "test",
                Password = "test"
            };

            var responseAuthentication = await client.PostAsync(
           BasePathAuthentication + "authenticate",
           new StringContent(JsonConvert.SerializeObject(authenticateRequest), Encoding.UTF8)
           {
               Headers = { ContentType = new MediaTypeHeaderValue("application/json") }

           });

            var jsonAsString = await responseAuthentication.Content.ReadAsStringAsync();
            AuthenticateResponse authenticateResponse = JsonConvert.DeserializeObject<AuthenticateResponse>(jsonAsString);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticateResponse.Token);
        }
    }
}

