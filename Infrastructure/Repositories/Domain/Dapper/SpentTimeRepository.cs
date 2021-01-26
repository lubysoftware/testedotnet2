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
    public class SpentTimeRepository : DomainRepository<SpentTime>,
                                  ISpentTimeRepository
    {
        public SpentTimeRepository(IDatabaseFactory databaseOptions) : base(databaseOptions)
        {
        }

        public SpentTimeRepository(IDbConnection databaseConnection, IDbTransaction transaction = null) : base(databaseConnection, transaction)
        {
        }

        protected override string InsertQuery => $@"INSERT INTO [{nameof(SpentTime)}]
                                                ({nameof(SpentTime.Id)},
                                                {nameof(SpentTime.Id)},
                                                {nameof(SpentTime.StartedAt)},
                                                {nameof(SpentTime.FinishedAt)},
                                                {nameof(SpentTime.CreatedAt)},
                                                {nameof(SpentTime.UpdatedAt)},
                                                {nameof(SpentTime.ProjectId)},
                                                {nameof(SpentTime.DeveloperId)})
                                                VALUES (
                                                NEWID(),
                                                @{nameof(SpentTime.StartedAt)},
                                                @{nameof(SpentTime.FinishedAt)},
                                                @{nameof(SpentTime.CreatedAt)},
                                                @{nameof(SpentTime.UpdatedAt)},
                                                @{nameof(SpentTime.ProjectId)},
                                                @{nameof(SpentTime.DeveloperId)})";
        protected override string InsertQueryReturnInserted => $@"INSERT INTO [{nameof(SpentTime)}] 
                                                ({nameof(SpentTime.Id)},
                                                {nameof(SpentTime.StartedAt)},
                                                {nameof(SpentTime.FinishedAt)},
                                                {nameof(SpentTime.CreatedAt)},
                                                {nameof(SpentTime.UpdatedAt)},
                                                {nameof(SpentTime.ProjectId)},
                                                {nameof(SpentTime.DeveloperId)}) 
                                                OUTPUT INSERTED.* VALUES (
                                                NEWID(),
                                                @{nameof(SpentTime.StartedAt)},
                                                @{nameof(SpentTime.FinishedAt)},
                                                @{nameof(SpentTime.CreatedAt)},
                                                @{nameof(SpentTime.UpdatedAt)},
                                                @{nameof(SpentTime.ProjectId)},
                                                @{nameof(SpentTime.DeveloperId)})";
        protected override string UpdateByIdQuery => "";
        protected override string DeleteByIdQuery => $"DELETE FROM [{nameof(SpentTime)}] WHERE {nameof(SpentTime.Id)} = @{nameof(SpentTime.Id)}";
        protected override string SelectAllQuery => $"SELECT * FROM [{nameof(SpentTime)}]";
        protected override string SelectByIdQuery => $"SELECT * FROM [{nameof(SpentTime)}] WHERE {nameof(SpentTime.Id)} = @{nameof(SpentTime.Id)}";

        public override Task<int> UpdateAsync(SpentTime obj)
        {
            throw new NotImplementedException();
        }

        public override Task<int> UpdateRangeAsync(IEnumerable<SpentTime> entities)
        {
            throw new NotImplementedException();
        }
    }
}
