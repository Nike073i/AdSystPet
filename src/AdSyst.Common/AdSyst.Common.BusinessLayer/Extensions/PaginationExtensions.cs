using AdSyst.Common.BusinessLayer.Options;

namespace AdSyst.Common.BusinessLayer.Extensions
{
    /// <summary>
    /// Методы-расширения для пагинации
    /// </summary>
    public static class PaginationExtensions
    {
        /// <summary>
        /// Получить страницу с данными
        /// </summary>
        /// <param name="queryable">Набор данных</param>
        /// <param name="options">Опции пагинации</param>
        /// <typeparam name="TSource">Тип данных в наборе</typeparam>
        /// <returns>Набор данных на указанной странице</returns>
        /// <exception cref="ArgumentOutOfRangeException">Исключение, возникающее в случае некорректных параметров пагинации</exception>
        public static IQueryable<TSource> Page<TSource>(
            this IQueryable<TSource> queryable,
            PaginationOptions options
        )
        {
            int pageSize = options.PageSize;
            int pageNumber = options.PageNumber;

            return pageSize < 0
                ? throw new ArgumentOutOfRangeException(
                    nameof(options),
                    "Размер страницы не может быть меньше нуля"
                )
                : pageNumber < 0
                    ? throw new ArgumentOutOfRangeException(
                        nameof(options),
                        "Текущая страница не может быть меньше нуля"
                    )
                    : queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
