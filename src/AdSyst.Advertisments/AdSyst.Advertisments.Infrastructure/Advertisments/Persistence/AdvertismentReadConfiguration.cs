using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Advertisments.Infrastructure.Data.Models;
using AdSyst.Common.Infrastructure.Data.Configurations;

namespace AdSyst.Advertisments.Infrastructure.Advertisments.Persistence
{
    internal class AdvertismentReadConfiguration
        : IEntityTypeConfiguration<AdvertismentReadModel>,
            IReadConfiguration
    {
        public void Configure(EntityTypeBuilder<AdvertismentReadModel> builder)
        {
            builder.ToTable("Advertisments");

            builder
                .Property(a => a.Status)
                .HasConversion(new EnumToStringConverter<AdvertismentStatus>());

            builder.HasOne(a => a.Category).WithMany().HasForeignKey(a => a.CategoryId);
            builder
                .HasOne(a => a.AdvertismentType)
                .WithMany()
                .HasForeignKey(a => a.AdvertismentTypeId);
            builder.HasMany(a => a.Images).WithOne().HasForeignKey(i => i.AdvertismentId);
        }
    }
}
