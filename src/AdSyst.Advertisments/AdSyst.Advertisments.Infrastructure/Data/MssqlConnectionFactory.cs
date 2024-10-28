using System.Data;
using Microsoft.Data.SqlClient;
using AdSyst.Common.Application.Abstractions.Data;

namespace AdSyst.Advertisments.Infrastructure.Data
{
    public class MssqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _connectionString;

        public MssqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection OpenConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
