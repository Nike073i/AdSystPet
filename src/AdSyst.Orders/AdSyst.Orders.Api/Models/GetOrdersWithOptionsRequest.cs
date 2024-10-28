using AdSyst.Common.BusinessLayer.Enums;
using AdSyst.Common.Presentation.Models;
using AdSyst.Orders.BusinessLayer.Enums;

namespace AdSyst.Orders.Api.Models
{
    /// <summary>
    /// Модель опций для получения заказов
    /// </summary>
    public record GetOrdersWithOptionsRequest : PageRequest
    {
        /// <summary>
        /// Направление сортировки
        /// </summary>
        public SortDirection? SortDirection { get; init; }

        /// <summary>
        /// Поле для сортировки
        /// </summary>
        public SortOrderField? SortField { get; init; }
    }
}
