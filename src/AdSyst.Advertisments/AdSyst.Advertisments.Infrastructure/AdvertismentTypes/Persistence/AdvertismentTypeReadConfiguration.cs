using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdSyst.Advertisments.Infrastructure.Data.Models;
using AdSyst.Common.Infrastructure.Data.Configurations;

namespace AdSyst.Advertisments.Infrastructure.AdvertismentTypes.Persistence
{
    internal class AdvertismentTypeReadConfiguration
        : IEntityTypeConfiguration<AdvertismentTypeReadModel>,
            IReadConfiguration
    {
        public void Configure(EntityTypeBuilder<AdvertismentTypeReadModel> builder)
        {
            builder.ToTable("AdvertismentTypes");

            builder
                .HasMany<AdvertismentReadModel>()
                .WithOne(a => a.AdvertismentType)
                .HasForeignKey(a => a.AdvertismentTypeId);
        }
    }
}
