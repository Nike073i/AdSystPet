using Microsoft.EntityFrameworkCore;
using AdSyst.Advertisments.Application.Categories.Queries.GetAllCategoriesQuery;
using AdSyst.Advertisments.Infrastructure.Data.Contexts;
using AdSyst.Advertisments.Infrastructure.Data.Models;

namespace AdSyst.Advertisments.Infrastructure.Categories.Services
{
    internal class GetCategoryListService : IGetCategoryListService
    {
        private readonly AdvertismentReadDbContext _dbContext;

        public GetCategoryListService(AdvertismentReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<CategoryViewModel>> GetAsync(
            CancellationToken cancellationToken
        )
        {
            // Загружаем все категории
            var categories = await _dbContext
                .Categories
                .AsTracking()
                .ToListAsync(cancellationToken);

            // Проходимся по корневым категориям. Дочерние категории будут сопоставлены автоматически
            var data = categories
                .Where(c => !c.ParentCategoryId.HasValue)
                .Select(MapToDto)
                .ToList();

            return data;
        }

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
