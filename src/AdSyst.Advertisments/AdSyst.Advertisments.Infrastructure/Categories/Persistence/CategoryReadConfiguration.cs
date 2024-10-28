using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdSyst.Advertisments.Infrastructure.Data.Models;
using AdSyst.Common.Infrastructure.Data.Configurations;

namespace AdSyst.Advertisments.Infrastructure.Categories.Persistence
{
    internal class CategoryReadConfiguration
        : IEntityTypeConfiguration<CategoryReadModel>,
            IReadConfiguration
    {
        public void Configure(EntityTypeBuilder<CategoryReadModel> builder)
        {
            builder.ToTable("Categories");

            builder
                .HasMany<AdvertismentReadModel>()
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId);

            builder
                .HasMany(c => c.Children)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentCategoryId);
        }
    }
}
