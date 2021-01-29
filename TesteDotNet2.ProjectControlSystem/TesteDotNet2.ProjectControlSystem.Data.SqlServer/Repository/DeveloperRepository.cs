using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TesteDotNet2.ProjectControlSystem.Data.SqlServer.Context;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;
using TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Repository;

namespace TesteDotNet2.ProjectControlSystem.Data.SqlServer.Repository
{
    public class DeveloperRepository : Repository<Developer>, IDeveloperRepository
    {
        public DeveloperRepository(ProjectControlSystemContext context)
        : base(context)
        {

        }

        public Developer GetByCPF(string cpf)
        {
            return DbSet.FirstOrDefault(x => x.CPF == cpf);
        }

        public List<ReportDeveloperResponse> GetRankingOfHoursWorked()
        {
            //return this.DbSet.Include(s => s.TimeSheets).Where(x => x.TimeSheets.Where(t => t.EndDate < DateTime.Now && t.EndDate < DateTime.Now.AddDays(-7)))).ToList();
            var sql = @"select top 5 sum(AmountOfHours) as Hours, d.Name from Developer d " +
                              "inner join timesheet t on t.DeveloperId = d.DeveloperId " +
                              "where AmountOfHours < 10                               " +
                              "and EndDate between GETUTCDATE() -7 and GETUTCDATE()   " +
                              "group by d.Name                                        " +
                              "order by 1 desc ";

            
            var developers = Db.Database.GetDbConnection().Query<ReportDeveloperResponse>(sql).ToList();
            return developers;

        }

        public async Task<MessageResponse> ValidateCPFAsync(string cpf)
        {
            string url = $"https://run.mocky.io/v3/067108b3-77a4-400b-af07-2db3141e95c9";

            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<MessageResponse>(json);
            }
        }
    }
}
