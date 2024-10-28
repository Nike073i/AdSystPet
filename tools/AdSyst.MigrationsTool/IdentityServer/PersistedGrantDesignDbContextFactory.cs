using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AdSyst.MigrationsTool.IdentityServer
{
    public class PersistedGrantDesignDbContextFactory
        : BaseDesignDbContextFactory,
            IDesignTimeDbContextFactory<PersistedGrantDbContext>
    {
        public PersistedGrantDbContext CreateDbContext(string[] args)
        {
            string connectionString =
                Configuration["IdentityServer:ConnectionString"]
                ?? throw new InvalidOperationException("Database connection string not set");

            string migrationAssembly =
                Configuration["IdentityServer:PersistedGrantMigrationsAssembly"]
                ?? throw new InvalidOperationException("Assembly with migrations not set");

            var dbOptionsBuilder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
            dbOptionsBuilder.UseSqlServer(
                connectionString,
                x => x.MigrationsAssembly(migrationAssembly)
            );
            return new PersistedGrantDbContext(dbOptionsBuilder.Options, new());
        }
    }
}
