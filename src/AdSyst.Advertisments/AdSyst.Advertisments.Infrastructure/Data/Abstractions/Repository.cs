using Microsoft.EntityFrameworkCore;
using AdSyst.Advertisments.Domain.Abstractions;
using AdSyst.Advertisments.Infrastructure.Data.Contexts;

namespace AdSyst.Advertisments.Infrastructure.Data.Abstractions
{
    internal abstract class Repository<TEntity, TId>
        where TEntity : Entity<TId>
    {
        protected readonly AdvertismentDbContext DbContext;

        protected Repository(AdvertismentDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected virtual IQueryable<TEntity> IncludeProperties(IQueryable<TEntity> entities) =>
            entities;

        public virtual void Add(TEntity entity) => DbContext.Add(entity);

        public virtual void Remove(TEntity entity) => DbContext.Remove(entity);

        public virtual Task<TEntity?> GetByIdAsync(
            TId id,
            bool includeRelationships,
            CancellationToken cancellationToken
        )
        {
            IQueryable<TEntity> queryable = DbContext.Set<TEntity>();

            if (includeRelationships)
                queryable = IncludeProperties(queryable);

            return queryable.FirstOrDefaultAsync(e => e.Id!.Equals(id), cancellationToken);
        }

        public Task<bool> IsExistsAsync(TId id, CancellationToken cancellationToken) =>
            DbContext
                .Set<TEntity>()
                .AsNoTracking()
                .AnyAsync(e => e.Id!.Equals(id), cancellationToken);
    }
}
