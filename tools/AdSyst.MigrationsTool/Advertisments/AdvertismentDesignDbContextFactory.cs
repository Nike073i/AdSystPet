using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using AdSyst.Advertisments.Infrastructure.Data.Contexts;
using AdSyst.Common.Infrastructure.Clock;

namespace AdSyst.MigrationsTool.Advertisments
{
    internal class AdvertismentDesignDbContextFactory
        : BaseDesignDbContextFactory,
            IDesignTimeDbContextFactory<AdvertismentDbContext>
    {
        public AdvertismentDbContext CreateDbContext(string[] args)
        {
            string connectionString =
                Configuration["Advertisments:ConnectionString"]
                ?? throw new InvalidOperationException("Database connection string not set");

            string migrationAssembly =
                Configuration["Advertisments:AdvertismentMigrationsAssembly"]
                ?? throw new InvalidOperationException("Assembly with migrations not set");

            var dbOptionsBuilder = new DbContextOptionsBuilder<AdvertismentDbContext>();
            dbOptionsBuilder.UseSqlServer(
                connectionString,
                x => x.MigrationsAssembly(migrationAssembly)
            );
            return new AdvertismentDbContext(dbOptionsBuilder.Options, new DateTimeProvider());
        }
    }
}
