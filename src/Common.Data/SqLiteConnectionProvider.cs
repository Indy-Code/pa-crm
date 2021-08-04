using System;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace Common.Data
{
    public class SqLiteConnectionProvider : IConnectionProvider
    {
        private readonly string _connectionString;

        public SqLiteConnectionProvider(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IDbConnection GetDbConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        public async Task<IDbConnection> GetOpenDbConnection()
        {
            var conn = new SQLiteConnection(_connectionString);
            await conn.OpenAsync()
                .ConfigureAwait(false);
            return conn;
        }
    }
}