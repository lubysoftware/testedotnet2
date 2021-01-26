using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Domain.Entities;
using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories.Domain; 
 using Microsoft.Extensions.Logging;
using Infrastructure.Repositories.Standard.Dapper;

namespace Infrastructure.Repositories.Domain.Dapper
{
    public class DeveloperRepository : DomainRepository<Developer>,
                                  IDeveloperDapperRepository
    {
        public DeveloperRepository(IDatabaseFactory databaseOptions) : base(databaseOptions)
        {
        }

        public DeveloperRepository(IDbConnection databaseConnection, IDbTransaction transaction = null) : base(databaseConnection, transaction)
        {
        }

        protected override string InsertQuery => "";
        protected override string InsertQueryReturnInserted => "";
        protected override string UpdateByIdQuery => "";
        protected override string DeleteByIdQuery => "";
        protected override string SelectAllQuery => "";
        protected override string SelectByIdQuery => "";
        protected string SelectTop5Query => @"SELECT TOP 5 d.* FROM Developers d
            INNER JOIN(
	            SELECT DeveloperId, AVG(DATEDIFF(SECOND, StartedAt, FinishedAt)) as Total 
	            FROM SpentTime WHERE StartedAt > DATEADD(WEEK,-1, GETDATE())
	            GROUP BY DeveloperId
            ) T ON t.DeveloperId = d.Id
            ORDER BY Total DESC";

        protected string InsertProjectQuery => "INSERT INTO DeveloperProject (DevelopersId, ProjectsId) VALUES (@developerId, @projectId)";

        public virtual async Task<IEnumerable<Developer>> GetTop5SpentTimeIdAsync()
        {
            return await dbConn.QueryAsync<Developer>(SelectTop5Query, transaction: dbTransaction);
        }

        public virtual async Task<int> AddDeveloperProjectAsync(Guid projectId, Guid developerId)
        {
            return await dbConn.ExecuteAsync(InsertProjectQuery, new { developerId, projectId }, transaction: dbTransaction);
        }
    }
}
