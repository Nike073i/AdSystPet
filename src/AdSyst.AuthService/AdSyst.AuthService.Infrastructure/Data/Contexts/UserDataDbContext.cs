using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AdSyst.AuthService.Domain;
using AdSyst.AuthService.Domain.Enums;

namespace AdSyst.AuthService.EfContext.UserData.Contexts
{
    public class UserDataDbContext : IdentityDbContext<AppUser>
    {
        public UserDataDbContext(DbContextOptions<UserDataDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder
                .Entity<IdentityRole>()
                .HasData(
                    new() { Id = "9c27c90c-d730-4822-bac1-06968fac690c", Name = nameof(Role.Client) },
                    new() { Id = "9f414ada-0a4b-4d90-aec5-21fce9109d8b", Name = nameof(Role.Editor) },
                    new()
                    {
                        Id = "6c44af13-1186-4ded-a05a-e18f9f1be6ed",
                        Name = nameof(Role.Moderator)
                    },
                    new() { Id = "544e9a99-77dc-44cb-95c2-7a3c50445acb", Name = nameof(Role.System) }
                );
        }
    }
}
