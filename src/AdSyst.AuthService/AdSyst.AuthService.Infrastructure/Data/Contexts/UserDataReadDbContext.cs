using Microsoft.EntityFrameworkCore;
using AdSyst.AuthService.EfContext.UserData.Configurations.ReadConfigurations;
using AdSyst.AuthService.EfContext.UserData.Models;

namespace AdSyst.AuthService.EfContext.UserData.Contexts
{
    public class UserDataReadDbContext : DbContext
    {
        public DbSet<AppUserReadModel> Users => Set<AppUserReadModel>();

        public UserDataReadDbContext(DbContextOptions<UserDataReadDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserReadModelConfiguration());
            modelBuilder.ApplyConfiguration(new RoleReadModelConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleReadModelConfiguration());
        }
    }
}
