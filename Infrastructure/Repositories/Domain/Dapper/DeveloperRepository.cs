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
                                  IDeveloperRepository
    {
        public DeveloperRepository(IDatabaseFactory databaseOptions) : base(databaseOptions)
        {
        }

        public DeveloperRepository(IDbConnection databaseConnection, IDbTransaction transaction = null) : base(databaseConnection, transaction)
        {
        }

        protected override string InsertQuery => "";//$"INSERT INTO [{nameof(Developer)}] ({nameof(Developer.Ano)}, {nameof(Developer.IBGE)}) VALUES (@{nameof(Developer.Nome)}, @{nameof(Developer.Nascimento)})";
        protected override string InsertQueryReturnInserted => "";//$"INSERT INTO [{nameof(Developer)}] ({nameof(Developer.Nome)}, {nameof(Developer.Nascimento)}) OUTPUT INSERTED.* VALUES (@{nameof(Developer.Nome)}, @{nameof(Developer.Nascimento)})";
        protected override string UpdateByIdQuery => "";//$"UPDATE [{nameof(Developer)}] SET {nameof(Developer.Nome)} = @{nameof(Developer.Nome)} WHERE {nameof(Developer.Id)} = @{nameof(Developer.Id)}";
        protected override string DeleteByIdQuery => "";//$"DELETE FROM [{nameof(Developer)}] WHERE {nameof(Developer.Id)} = @{nameof(Developer.Id)}";
        protected override string SelectAllQuery => "";//$"SELECT * FROM [{nameof(Developer)}]";
        protected override string SelectByIdQuery => "";//$"SELECT * FROM [{nameof(Developer)}] WHERE {nameof(Developer.Id)} = @{nameof(Developer.Id)}";
    }
}
