using System.Linq.Expressions;
using AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentList;
using AdSyst.Advertisments.Infrastructure.Data.Models;
using AdSyst.Common.BusinessLayer.Enums;

namespace AdSyst.Advertisments.Infrastructure.Advertisments.Persistence
{
    /// <summary>
    /// "Объект-запрос" для сортировки объявлений.
    /// <see href="http://design-pattern.ru/patterns/query-object.html"/>
    /// </summary>
    internal static class AdvertismentSortExtensions
    {
        /// <summary>
        /// Сортировка объявлений
        /// </summary>
        /// <param name="queryable">Набор объявлений</param>
        /// <param name="filterOptions">Опции сортировки</param>
        /// <returns>Набор объявлений</returns>
        internal static IQueryable<AdvertismentReadModel> Sort(
            this IQueryable<AdvertismentReadModel> queryable,
            AdvertismentSortOptions sortOptions
        )
        {
            Expression<Func<AdvertismentReadModel, object>> sortFieldSelector =
                sortOptions.SortField switch
                {
                    SortAdvertismentField.Id => a => a.Id,
                    SortAdvertismentField.Title => a => a.Title,
                    SortAdvertismentField.Price => a => a.Price,
                    SortAdvertismentField.DateOfPublish => a => a.CreatedAt,
                    _ => throw new InvalidOperationException(),
                };
            return sortOptions.SortDirection == SortDirection.Asc
                ? queryable.OrderBy(sortFieldSelector)
                : queryable.OrderByDescending(sortFieldSelector);
        }
    }
}
