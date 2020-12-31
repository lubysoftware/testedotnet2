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

        public async Task<Result<bool>> SendNotification(string title, string message)
        { 
            var result = await GetAsync("a1b59b8e-577d-4996-a4c5-56215907d9dd");
            return new Result<bool>(result.Status, result.ErrorMessages, result.Data == "Enviado");
        }

        public async Task<Result<bool>> ValidateCPF(string cpf)
        {
            var result = await GetAsync("067108b3-77a4-400b-af07-2db3141e95c9");
            return new Result<bool>(result.Status, result.ErrorMessages, result.Data == "Autorizado");
        }

        private async Task<Result<string>> GetAsync(string route)
        {
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(_mockyConfiguration.Url) };
                var response = await client.GetAsync(route);
                var stream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<MockyResponseDto>(stream);
                return new Result<string>(result.message);
            } catch
            {
                return new Result<string>(Status.Error, "Erro to comunicate with Mocky");
            }
        }
    }
}
