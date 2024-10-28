using Microsoft.EntityFrameworkCore;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Advertisments.Infrastructure.Data.Abstractions;
using AdSyst.Advertisments.Infrastructure.Data.Contexts;

namespace AdSyst.Advertisments.Infrastructure.Advertisments.Persistence
{
    internal class AdvertismentRepository : Repository<Advertisment, Guid>, IAdvertismentRepository
    {
        public AdvertismentRepository(AdvertismentDbContext dbContext)
            : base(dbContext) { }

        public Task<bool> AnyAdvertismentsWithCategory(
            Guid id,
            CancellationToken cancellationToken
        ) => DbContext.Advertisments.AnyAsync(a => a.CategoryId == id, cancellationToken);

        public Task<bool> AnyAdvertismentsWithType(Guid id, CancellationToken cancellationToken) =>
            DbContext
                .Advertisments
                .AsNoTracking()
                .AnyAsync(a => a.AdvertismentTypeId == id, cancellationToken);

        protected override IQueryable<Advertisment> IncludeProperties(
            IQueryable<Advertisment> entities
        ) => entities.Include(ad => ad.Images);
    }
}
