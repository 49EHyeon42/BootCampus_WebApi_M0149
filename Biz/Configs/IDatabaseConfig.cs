using System.Data.Common;

namespace Biz.Configs
{
    public interface IDatabaseConfig
    {
        public DbConnection GetDbConnection();
    }
}
