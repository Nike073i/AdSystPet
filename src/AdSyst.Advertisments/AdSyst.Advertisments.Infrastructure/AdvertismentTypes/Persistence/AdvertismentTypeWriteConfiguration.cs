using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdSyst.Advertisments.Domain.AdvertismentTypes;
using AdSyst.Common.Infrastructure.Data.Configurations;

namespace AdSyst.Advertisments.Infrastructure.AdvertismentTypes.Persistence
{
    /// <summary>
    /// Конфигурация сущности типа объявления
    /// </summary>
    internal class AdvertismentTypeWriteConfiguration
        : IEntityTypeConfiguration<AdvertismentType>,
            IWriteConfiguration
    {
        public void Configure(EntityTypeBuilder<AdvertismentType> builder) =>
            builder.Property(e => e.Name).HasMaxLength(50);
    }
}
