using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdSyst.Advertisments.Infrastructure.Data.Models;
using AdSyst.Common.Infrastructure.Data.Configurations;

namespace AdSyst.Advertisments.Infrastructure.Advertisments.Persistence
{
    internal class AdvertismentImageReadConfiguration
        : IEntityTypeConfiguration<AdvertismentImageReadModel>,
            IReadConfiguration
    {
        public void Configure(EntityTypeBuilder<AdvertismentImageReadModel> builder)
        {
            builder.ToTable("AdvertismentImages");
            builder
                .HasOne<AdvertismentReadModel>()
                .WithMany(a => a.Images)
                .HasForeignKey(i => i.AdvertismentId);
        }
    }
}
