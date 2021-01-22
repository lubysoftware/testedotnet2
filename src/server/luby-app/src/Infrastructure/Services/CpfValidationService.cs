using luby_app.Application.Common.Interfaces;
using luby_app.Application.Common.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace luby_app.Infrastructure.Services
{
    public class CpfValidationService : ICpfValidationService
    {
        private readonly HttpClient _httpClient; 

        public CpfValidationService(HttpClient httpClient)
        {
            _httpClient = httpClient; 
        }

        public async Task<bool> IsValid(string cpf)
        {
            IntegrationResult result = await _httpClient.GetFromJsonAsync<IntegrationResult>("");

            return result.Message == "Autorizado";
        }
    }
}
