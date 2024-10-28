using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Advertisments.Domain.Advertisments.AdvertismentStates;
using AdSyst.Advertisments.Domain.AdvertismentTypes;
using AdSyst.Advertisments.Domain.Categories;
using AdSyst.Common.Infrastructure.Data.Configurations;

namespace AdSyst.Advertisments.Infrastructure.Advertisments.Persistence
{
    /// <summary>
    /// Конфигурация сущности объявления
    /// </summary>
    internal class AdvertismentWriteConfiguration
        : IEntityTypeConfiguration<Advertisment>,
            IWriteConfiguration
    {
        public void Configure(EntityTypeBuilder<Advertisment> builder)
        {
            builder
                .Property<IAdvertismentState>("_state")
                .HasColumnName("Status")
                .HasConversion<AdvertismentStatusConverter>();

            builder.Property(e => e.Title).HasMaxLength(50);
            builder.Property(e => e.Description).HasMaxLength(1000);

            builder.HasMany(a => a.Images).WithOne().HasForeignKey("AdvertismentId");

            builder
                .HasOne<AdvertismentType>()
                .WithMany()
                .HasForeignKey(a => a.AdvertismentTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne<Category>()
                .WithMany()
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
