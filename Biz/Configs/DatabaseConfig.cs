using System.Data;
using Microsoft.Data.SqlClient;

namespace Biz.Configs
{
    public class DatabaseConfig
    {
        public required string DefaultConnectionString { private get; init; }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(DefaultConnectionString);
        }
    }
}
