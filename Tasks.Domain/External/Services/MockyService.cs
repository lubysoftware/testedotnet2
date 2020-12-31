using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Tasks.Domain._Common.Enums;
using Tasks.Domain._Common.External;
using Tasks.Domain._Common.Results;
using Tasks.Domain.External.Dtos;

namespace Tasks.Domain.External.Services
{
    public class MockyService : IMockyService
    {
        private readonly MockyConfiguration _mockyConfiguration;

        public MockyService(MockyConfiguration mockyConfiguration)
        {
            _mockyConfiguration = mockyConfiguration;
        }

        public async Task<Result> SendNotification(string title, string message)
        { 
            var result = await GetAsync("a1b59b8e-577d-4996-a4c5-56215907d9dd");
            return new Result(result.Message == "Enviado" ? Status.Success : Status.Error, result.Message);
        }

        public async Task<Result> ValidateCPF(string cpf)
        {
            var result = await GetAsync("067108b3-77a4-400b-af07-2db3141e95c9");
            return new Result(result.Message == "Autorizado" ? Status.Success : Status.Error, result.Message);
        }

        private async Task<MockyResponseDto> GetAsync(string route)
        {
            var client = new HttpClient { BaseAddress = new Uri(_mockyConfiguration.Url) };
            var response = await client.GetAsync(route);
            var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<MockyResponseDto>(stream);
        }
    }
}
