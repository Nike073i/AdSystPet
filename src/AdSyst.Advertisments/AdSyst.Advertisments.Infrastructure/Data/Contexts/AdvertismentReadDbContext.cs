using System.Reflection;
using Microsoft.EntityFrameworkCore;
using AdSyst.Advertisments.Infrastructure.Data.Models;
using AdSyst.Common.Infrastructure.Data.Configurations;

namespace AdSyst.Advertisments.Infrastructure.Data.Contexts
{
    internal class AdvertismentReadDbContext : DbContext
    {
        public AdvertismentReadDbContext(
            DbContextOptions<AdvertismentReadDbContext> dbContextOptions
        )
            : base(dbContextOptions)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public virtual DbSet<AdvertismentTypeReadModel> AdvertismentTypes =>
            Set<AdvertismentTypeReadModel>();

        public virtual DbSet<CategoryReadModel> Categories => Set<CategoryReadModel>();

        public virtual DbSet<AdvertismentReadModel> Advertisments => Set<AdvertismentReadModel>();

        public virtual DbSet<AdvertismentImageReadModel> AdvertismentImages =>
            Set<AdvertismentImageReadModel>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly(),
                type => type.IsAssignableTo(typeof(IReadConfiguration))
            );
        }
    }
}
