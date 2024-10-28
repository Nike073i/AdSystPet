using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdSyst.AuthService.EfContext.UserData.Models;

namespace AdSyst.AuthService.EfContext.UserData.Configurations.ReadConfigurations
{
    public class RoleReadModelConfiguration : IEntityTypeConfiguration<RoleReadModel>
    {
        public void Configure(EntityTypeBuilder<RoleReadModel> builder) =>
            builder.ToTable("AspNetRoles", e => e.ExcludeFromMigrations());
    }
}
