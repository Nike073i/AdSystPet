using AdSyst.Common.BusinessLayer.Enums;
using AdSyst.Orders.BusinessLayer.Enums;

namespace AdSyst.Orders.BusinessLayer.Extensions.Sorting
{
    /// <summary>
    /// Опции сортировки заказов
    /// </summary>
    public record OrderSortOptions
    {
        /// <summary>
        /// Поле сортировки
        /// </summary>
        /// <value>По умолчанию сортировка по дате создания</value>
        public SortOrderField SortField { get; }

        /// <summary>
        /// Направление сортировки
        /// </summary>
        /// <value>По умолчанию сортировка по убыванию</value>
        public SortDirection SortDirection { get; }

        public OrderSortOptions(
            SortOrderField? sortField = null,
            SortDirection? sortDirection = null
        )
        {
            SortField = sortField ?? SortOrderField.Date;
            SortDirection = sortDirection ?? SortDirection.Desc;
        }
    }
}
