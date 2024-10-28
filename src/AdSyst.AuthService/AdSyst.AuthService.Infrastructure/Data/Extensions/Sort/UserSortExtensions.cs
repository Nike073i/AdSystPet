using System.Linq.Expressions;
using AdSyst.AuthService.Application.Users.Queries.GetUserInfoList;
using AdSyst.AuthService.EfContext.UserData.Models;
using AdSyst.Common.BusinessLayer.Enums;

namespace AdSyst.AuthService.EfContext.UserData.Extensions.Sort
{
    /// <summary>
    /// "Объект-запрос" для сортировки пользователей.
    /// <see href="http://design-pattern.ru/patterns/query-object.html"/>
    /// </summary>
    public static class UserSortExtensions
    {
        /// <summary>
        /// Сортировка пользователей
        /// </summary>
        /// <param name="queryable">Набор пользователей</param>
        /// <param name="filterOptions">Опции сортировки</param>
        /// <returns>Набор объявлений</returns>
        public static IQueryable<AppUserReadModel> Sort(
            this IQueryable<AppUserReadModel> queryable,
            UserSortOptions sortOptions
        )
        {
            Expression<Func<AppUserReadModel, object>> sortFieldSelector =
                sortOptions.SortField switch
                {
                    SortUserField.Id => u => u.Id,
                    SortUserField.Email => u => u.Email!,
                    _ => throw new InvalidOperationException(),
                };
            return sortOptions.SortDirection == SortDirection.Asc
                ? queryable.OrderBy(sortFieldSelector)
                : queryable.OrderByDescending(sortFieldSelector);
        }
    }
}
