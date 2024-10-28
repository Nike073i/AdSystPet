using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdSyst.Advertisments.Domain.Categories;
using AdSyst.Common.Infrastructure.Data.Configurations;

namespace AdSyst.Advertisments.Infrastructure.Categories.Persistence
{
    /// <summary>
    /// Конфигурация сущности категории
    /// </summary>
    internal class CategoryWriteConfiguration
        : IEntityTypeConfiguration<Category>,
            IWriteConfiguration
    {
        public void Configure(EntityTypeBuilder<Category> builder) =>
            builder.Property(e => e.Name).HasMaxLength(50);
    }
}
