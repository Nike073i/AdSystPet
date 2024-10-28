using Microsoft.EntityFrameworkCore;
using AdSyst.Advertisments.Domain.Categories;
using AdSyst.Advertisments.Infrastructure.Data.Abstractions;
using AdSyst.Advertisments.Infrastructure.Data.Contexts;

namespace AdSyst.Advertisments.Infrastructure.Categories.Persistence
{
    internal class CategoryRepository : Repository<Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(AdvertismentDbContext dbContext)
            : base(dbContext) { }

        protected override IQueryable<Category> IncludeProperties(IQueryable<Category> entities) =>
            entities.Include(c => c.Children);
    }
}
