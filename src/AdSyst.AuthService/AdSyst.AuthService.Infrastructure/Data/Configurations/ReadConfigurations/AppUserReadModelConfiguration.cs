using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdSyst.AuthService.EfContext.UserData.Models;

namespace AdSyst.AuthService.EfContext.UserData.Configurations.ReadConfigurations
{
    public class AppUserReadModelConfiguration : IEntityTypeConfiguration<AppUserReadModel>
    {
        public void Configure(EntityTypeBuilder<AppUserReadModel> builder) =>
            builder.ToTable("AspNetUsers", e => e.ExcludeFromMigrations());
    }
}
