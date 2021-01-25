using System.Data;
using Microsoft.Data.SqlClient;

namespace UnitTest.Integration.Repositories.DBConfiguration
{
    public class DataOptionFactory
    {
        public string DefaultConnection { get; set; }
        public IDbConnection DatabaseConnection => new SqlConnection(DefaultConnection);
    }
}