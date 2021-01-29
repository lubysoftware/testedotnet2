using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TesteDotNet2.ProjectControlSystem.Data.SqlServer.Context;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;
using TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Repository;

namespace TesteDotNet2.ProjectControlSystem.Data.SqlServer.Repository
{
    public class TimeSheetRepository : Repository<TimeSheet>, ITimeSheetRepository
    {
        public TimeSheetRepository(ProjectControlSystemContext context)
        : base(context)
        {

        }    

        public async Task<MessageResponse> Notify(Guid developerId)
        {
            string url = $"https://run.mocky.io/v3/a1b59b8e-577d-4996-a4c5-56215907d9dd";

            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<MessageResponse>(json);
            }
        }
    }
}
