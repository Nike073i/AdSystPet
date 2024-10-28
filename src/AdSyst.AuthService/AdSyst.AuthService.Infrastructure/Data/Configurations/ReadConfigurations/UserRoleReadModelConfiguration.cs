using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdSyst.AuthService.EfContext.UserData.Models;

namespace AdSyst.AuthService.EfContext.UserData.Configurations.ReadConfigurations
{
    public class UserRoleReadModelConfiguration : IEntityTypeConfiguration<UserRoleReadModel>
    {
        public void Configure(EntityTypeBuilder<UserRoleReadModel> builder)
        {
            builder.ToTable("AspNetUserRoles", e => e.ExcludeFromMigrations());
            builder.HasKey(e => new { e.UserId, e.RoleId });
        }
    }
}
