using DTO.Request;
using DTO.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Luby.TimeManager.API.Tests.Context
{
    class TestContext
    {
        public HttpClient Client { get; set; }
        private TestServer _server;
        public TestContext()
        {
            SetupClient();
        }
        private void SetupClient()
        {


            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json")
                .AddEnvironmentVariables();
            var configuration = builder.Build();

            _server = new TestServer(new WebHostBuilder().UseConfiguration(configuration).UseStartup<Startup>());
            Client = _server.CreateClient();
            //var token = GetToken();
            //Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }

        /*private string GetToken()
        {
            var user = new AuthUserRequestDTO
            {
                login = "user@example.com",
                password = "string"
            };

            var request = Client.PostAsJsonAsync("/Account/login", user).Result;
            var response = request.Content.ReadAsStringAsync().Result;

            var obj = JsonConvert.DeserializeObject<AuthUserResponseDTO>(response);

            return obj.AccessToken;
        }*/
    }
}
