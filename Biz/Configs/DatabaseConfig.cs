using System.Data;
using Microsoft.Data.SqlClient;

namespace Biz.Configs
{
    public class DatabaseConfig
    {
        private readonly string _connectionString;

        public DatabaseConfig(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
