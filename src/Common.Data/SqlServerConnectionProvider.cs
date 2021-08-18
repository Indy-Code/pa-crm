using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Common.Data
{
    public class SqlServerConnectionProvider : IConnectionProvider
    {
        private readonly string _connectionString;
        public SqlServerConnectionProvider(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public IDbConnection GetDbConnection()
        {
            return new SqlConnection(_connectionString);
        }
        
        public async Task<IDbConnection> GetOpenDbConnection()
        {
            var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync()
                .ConfigureAwait(false);
            return conn;
        }
    }
}