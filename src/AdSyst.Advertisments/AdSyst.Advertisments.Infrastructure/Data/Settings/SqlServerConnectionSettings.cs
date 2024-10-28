using Microsoft.Data.SqlClient;

namespace AdSyst.Advertisments.Infrastructure.Data.Settings
{
    internal class SqlServerConnectionSettings
    {
        public required string Password { get; set; }
        public required string UserId { get; set; }
        public required string DataSource { get; set; }
        public required string InitialCatalog { get; set; }
        public bool TrustServerCertificate { get; set; }

        public string GetConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                UserID = UserId,
                Password = Password,
                DataSource = DataSource,
                InitialCatalog = InitialCatalog,
                TrustServerCertificate = TrustServerCertificate,
            };
            return builder.ConnectionString;
        }
    }
}
