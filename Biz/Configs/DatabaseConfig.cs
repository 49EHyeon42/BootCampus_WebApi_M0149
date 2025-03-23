using System.Data;
using Microsoft.Data.SqlClient;

namespace Biz.Configs
{
    public class DatabaseConfig
    {
        public required string ConnectionString { private get; init; }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
