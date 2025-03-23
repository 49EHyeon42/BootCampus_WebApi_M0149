using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace Biz.Configs
{
    public class MsSqlConfig(string connectionString) : IDatabaseConfig
    {
        private readonly string _connectionString = connectionString;

        public DbConnection GetDbConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
