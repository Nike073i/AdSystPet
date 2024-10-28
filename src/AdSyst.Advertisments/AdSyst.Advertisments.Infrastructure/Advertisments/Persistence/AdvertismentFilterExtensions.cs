using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Advertisments.Infrastructure.Advertisments.Services;
using AdSyst.Advertisments.Infrastructure.Data.Models;
using AdSyst.Common.BusinessLayer.Extensions;

namespace AdSyst.Advertisments.Infrastructure.Advertisments.Persistence
{
    /// <summary>
    /// "Объект-запрос" для фильтрации объявлений.
    /// <see href="http://design-pattern.ru/patterns/query-object.html"/>
    /// </summary>
    internal static class AdvertismentFilterExtensions
    {
        /// <summary>
        /// Фильтрация объявлений
        /// </summary>
        /// <param name="queryable">Набор объявлений</param>
        /// <param name="filterOptions">Опции фильтрации</param>
        /// <returns>Набор объявлений</returns>
        internal static IQueryable<AdvertismentReadModel> Filter(
            this IQueryable<AdvertismentReadModel> queryable,
            AdvertismentFilterOptions? filterOptions
        ) =>
            filterOptions == null
                ? queryable
                : queryable
                    .FilterByUser(filterOptions.UserId)
                    .FilterByDate(filterOptions.PeriodStart, filterOptions.PeriodEnd)
                    .FilterBySearchWord(filterOptions.Search)
                    .FilterByCategories(filterOptions.CategoryIds)
                    .FilterByStatus(filterOptions.Status);

        /// <summary>
        /// Фильтрация по дате создания
        /// </summary>
        /// <param name="queryable">Набор объявлений</param>
        /// <param name="periodStart">Дата начала периода</param>
        /// <param name="periodEnd">Дата окончания периода</param>
        /// <returns>Набор объявлений</returns>
        private static IQueryable<AdvertismentReadModel> FilterByDate(
            this IQueryable<AdvertismentReadModel> queryable,
            DateTimeOffset? periodStart,
            DateTimeOffset? periodEnd
        )
        {
            if (periodStart.HasValue)
                queryable = queryable.Where(a => a.CreatedAt >= periodStart.Value);
            if (periodEnd.HasValue)
                queryable = queryable.Where(a => a.CreatedAt <= periodEnd.Value);
            return queryable;
        }

        /// <summary>
        /// Фильтрация по собственнику
        /// </summary>
        /// <param name="queryable">Набор объявлений</param>
        /// <param name="userId">Идентификатор собственника</param>
        /// <returns>Набор объявлений</returns>
        private static IQueryable<AdvertismentReadModel> FilterByUser(
            this IQueryable<AdvertismentReadModel> queryable,
            Guid? userId
        ) => userId.HasValue ? queryable.Where(a => a.UserId == userId) : queryable;

        /// <summary>
        /// Фильтрация по поисковой фразе
        /// </summary>
        /// <value>
        /// Возвращает объявления, название которых содержит поисковую фразу
        /// </value>
        /// <param name="queryable">Набор объявлений</param>
        /// <param name="search">Поисковая фраза</param>
        /// <returns>Набор объявлений</returns>
        private static IQueryable<AdvertismentReadModel> FilterBySearchWord(
            this IQueryable<AdvertismentReadModel> queryable,
            string? search
        ) =>
            string.IsNullOrEmpty(search)
                ? queryable
                : queryable.Where(a => a.Title.Contains(search));

        /// <summary>
        /// Фильтрация по категориям
        /// </summary>
        /// <value>
        /// Возвращает объявления, категория которых входит в перечисление <paramref name="categoryIds"/>
        /// </value>
        /// <param name="queryable">Набор объявлений</param>
        /// <param name="categoryIds">Перечисление идентификаторов категорий</param>
        /// <returns>Набор объявлений</returns>
        private static IQueryable<AdvertismentReadModel> FilterByCategories(
            this IQueryable<AdvertismentReadModel> queryable,
            IEnumerable<Guid>? categoryIds
        ) =>
            categoryIds.IsNullOrEmpty()
                ? queryable
                : queryable.Where(a => categoryIds!.Any(id => a.CategoryId == id));

        /// <summary>
        /// Фильтрация по статусу объявления
        /// </summary>
        /// <value>
        /// Возвращает объявления, статус которых соответствует <paramref name="status"/>
        /// </value>
        /// <param name="queryable">Набор объявлений</param>
        /// <param name="status">Статус объявления</param>
        /// <returns>Набор объявлений</returns>
        private static IQueryable<AdvertismentReadModel> FilterByStatus(
            this IQueryable<AdvertismentReadModel> queryable,
            AdvertismentStatus? status
        ) => !status.HasValue ? queryable : queryable.Where(a => a.Status == status);
    }
}
