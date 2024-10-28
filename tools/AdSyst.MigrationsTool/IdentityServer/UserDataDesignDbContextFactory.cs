using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using AdSyst.AuthService.EfContext.UserData.Contexts;

namespace AdSyst.MigrationsTool.IdentityServer
{
    public class UserDataDesignDbContextFactory
        : BaseDesignDbContextFactory,
            IDesignTimeDbContextFactory<UserDataDbContext>
    {
        public UserDataDbContext CreateDbContext(string[] args)
        {
            string connectionString =
                Configuration["IdentityServer:ConnectionString"]
                ?? throw new InvalidOperationException("Database connection string not set");

            string migrationAssembly =
                Configuration["IdentityServer:UserDataMigrationsAssembly"]
                ?? throw new InvalidOperationException("Assembly with migrations not set");

            var dbOptionsBuilder = new DbContextOptionsBuilder<UserDataDbContext>();
            dbOptionsBuilder.UseSqlServer(
                connectionString,
                x => x.MigrationsAssembly(migrationAssembly)
            );
            return new UserDataDbContext(dbOptionsBuilder.Options);
        }
    }
}
