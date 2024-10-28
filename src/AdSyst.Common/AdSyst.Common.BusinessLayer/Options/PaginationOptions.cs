namespace AdSyst.Common.BusinessLayer.Options
{
    /// <summary>
    /// Опции пагинации
    /// </summary>
    public record PaginationOptions
    {
        /// <summary>
        /// Количество элементов на странице
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Номер страницы
        /// </summary>
        public int PageNumber { get; }

        public PaginationOptions(int? pageSize = null, int? pageNumber = null)
        {
            PageSize = pageSize ?? 10;
            PageNumber = pageNumber ?? 1;
        }
    }
}
