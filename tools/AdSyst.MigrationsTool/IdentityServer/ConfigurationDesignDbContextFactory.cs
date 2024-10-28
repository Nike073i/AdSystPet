using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AdSyst.MigrationsTool.IdentityServer
{
    public class ConfigurationDesignDbContextFactory
        : BaseDesignDbContextFactory,
            IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext CreateDbContext(string[] args)
        {
            string connectionString =
                Configuration["IdentityServer:ConnectionString"]
                ?? throw new InvalidOperationException("Database connection string not set");

            string migrationAssembly =
                Configuration["IdentityServer:ConfigurationMigrationsAssembly"]
                ?? throw new InvalidOperationException("Assembly with migrations not set");

            var dbOptionsBuilder = new DbContextOptionsBuilder<ConfigurationDbContext>();
            dbOptionsBuilder.UseSqlServer(
                connectionString,
                x => x.MigrationsAssembly(migrationAssembly)
            );
            return new ConfigurationDbContext(dbOptionsBuilder.Options, new());
        }
    }
}
