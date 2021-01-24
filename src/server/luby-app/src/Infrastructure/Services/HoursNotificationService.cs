using luby_app.Application.Common.Interfaces;
using luby_app.Application.Common.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace luby_app.Infrastructure.Services
{
    public class HoursNotificationService : IHoursNotificationService
    {
        private readonly HttpClient _httpClient;

        public HoursNotificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Send()
        {
            IntegrationResult result = await _httpClient.GetFromJsonAsync<IntegrationResult>("");

            return result.Message == "Enviado";
        }
    }
}
