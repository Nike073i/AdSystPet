using Microsoft.EntityFrameworkCore;
using AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentList;
using AdSyst.Advertisments.Infrastructure.Advertisments.Persistence;
using AdSyst.Advertisments.Infrastructure.Data.Contexts;
using AdSyst.Advertisments.Infrastructure.Data.Models;
using AdSyst.Common.BusinessLayer.Extensions;
using AdSyst.Common.BusinessLayer.Options;

namespace AdSyst.Advertisments.Infrastructure.Advertisments.Services
{
    internal class GetAdvertismentListService : IGetAdvertismentListService
    {
        private readonly AdvertismentReadDbContext _dbContext;

        public GetAdvertismentListService(AdvertismentReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<AdvertismentViewModel>> GetAsync(
            AdvertismentFilterDto? filterOptions,
            AdvertismentSortOptions sortOptions,
            PaginationOptions paginationOptions,
            CancellationToken cancellationToken
        )
        {
            var filter = await CreateFilterOptions(filterOptions, cancellationToken);

            return await _dbContext
                .Advertisments
                .Filter(filter)
                .Sort(sortOptions)
                .Page(paginationOptions)
                .Select(
                    ad =>
                        new AdvertismentViewModel(
                            ad.Id,
                            ad.Title,
                            ad.Price,
                            ad.Status,
                            ad.Category!.Id,
                            ad.Category.Name,
                            ad.Images!.IsNullOrEmpty()
                                ? null
                                : ad.Images!.OrderBy(i => i.Order).Select(i => i.ImageId).First(),
                            ad.CreatedAt
                        )
                )
                .ToListAsync(cancellationToken);
        }

        private async Task<AdvertismentFilterOptions?> CreateFilterOptions(
            AdvertismentFilterDto? dto,
            CancellationToken cancellationToken
        )
        {
            if (dto == null)
                return null;

            var categoryIds = dto.CategoryId.HasValue
                ? await GetRootAndChildrenIdsAsync(dto.CategoryId.Value, cancellationToken)
                : null;

            return new(
                dto.Search,
                categoryIds,
                dto.PeriodStart,
                dto.PeriodEnd,
                dto.UserId,
                dto.Status
            );
        }

        public async Task<Guid[]> GetRootAndChildrenIdsAsync(
            Guid rootId,
            CancellationToken cancellationToken
        )
        {
            var root = await _dbContext
                .Categories
                .AsTracking()
                .FirstOrDefaultAsync(c => c.Id == rootId, cancellationToken);
            if (root == null)
                return Array.Empty<Guid>();

            // Подгрузка всех записей для установки связанных сущностей в навигационных свойствах
            await _dbContext.Categories.AsTracking().LoadAsync(cancellationToken);

            var ids = ExploreHierarchy(root);
            return ids.ToArray();
        }

        // Обхож иерархии в глубину
        private static HashSet<Guid> ExploreHierarchy(CategoryReadModel root)
        {
            var ids = new HashSet<Guid>();
            var stack = new Stack<CategoryReadModel>();
            stack.Push(root);

            while (stack.Any())
            {
                var node = stack.Pop();
                ids.Add(node.Id);

                var children = node.Children;
                if (children.IsNullOrEmpty())
                    continue;

                foreach (var child in children!)
                    stack.Push(child);
            }
            return ids;
        }
    }
}
