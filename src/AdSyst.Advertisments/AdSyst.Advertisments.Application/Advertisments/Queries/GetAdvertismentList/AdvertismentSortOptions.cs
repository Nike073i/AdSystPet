using AdSyst.Common.BusinessLayer.Enums;

namespace AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentList
{
    /// <summary>
    /// Опции сортировки объявленний
    /// </summary>
    public record AdvertismentSortOptions
    {
        /// <summary>
        /// Поле сортировки объявления
        /// </summary>
        /// <value>По умолчанию сортировка по дате публикации</value>
        public SortAdvertismentField SortField { get; }

        /// <summary>
        /// Направление сортировки
        /// </summary>
        /// <value>По умолчанию сортировка по убыванию</value>
        public SortDirection SortDirection { get; }

        public AdvertismentSortOptions(
            SortAdvertismentField? sortField = null,
            SortDirection? sortDirection = null
        )
        {
            SortField = sortField ?? SortAdvertismentField.DateOfPublish;
            SortDirection = sortDirection ?? SortDirection.Desc;
        }
    }
}
