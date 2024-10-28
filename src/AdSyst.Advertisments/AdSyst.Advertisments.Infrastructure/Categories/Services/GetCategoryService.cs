using Microsoft.EntityFrameworkCore;
using AdSyst.Advertisments.Application.Categories.Queries.GetCategoryById;
using AdSyst.Advertisments.Infrastructure.Data.Contexts;
using AdSyst.Advertisments.Infrastructure.Data.Models;

namespace AdSyst.Advertisments.Infrastructure.Categories.Services
{
    internal class GetCategoryService : IGetCategoryService
    {
        private readonly AdvertismentReadDbContext _dbContext;

        public GetCategoryService(AdvertismentReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<CategoryViewModel?> GetAsync(Guid id, CancellationToken cancellationToken) =>
            _dbContext
                .Categories
                .Include(c => c.Children)
                .Where(c => c.Id == id)
                .Select(c => MapToDto(c))
                .FirstOrDefaultAsync(cancellationToken);

        private static CategoryViewModel MapToDto(CategoryReadModel category)
        {
            var children =
                category.Children != null
                    ? category.Children.Select(MapToDto)
                    : Enumerable.Empty<CategoryViewModel>();
            return new(category.Id, category.Name, children);
        }
    }
}
