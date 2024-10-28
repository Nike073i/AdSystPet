using MediatR;
using AdSyst.Common.BusinessLayer.Enums;
using AdSyst.Common.BusinessLayer.Options;
using AdSyst.Orders.BusinessLayer.Enums;
using AdSyst.Orders.BusinessLayer.Extensions.Filter;
using AdSyst.Orders.BusinessLayer.Extensions.Sorting;
using AdSyst.Orders.DAL.MongoDb.Enums;

namespace AdSyst.Orders.Application.Orders.Queries.GetOrderList
{
    public record GetOrderListQuery(
        OrderSortOptions SortOptions,
        PaginationOptions PaginationOptions,
        OrderFilterOptions? FilterOptions = null
    ) : IRequest<GetOrderListQueryResponse>
    {
        public static GetOrderListQuery CreateQuery(
            SortOrderField? sortField = null,
            SortDirection? sortDirection = null,
            int? pageSize = null,
            int? pageNumber = null,
            DateTimeOffset? periodStart = null,
            DateTimeOffset? periodEnd = null,
            OrderStatus? status = null,
            Guid? sellerId = null,
            Guid? buyerId = null
        )
        {
            var sortOptions = new OrderSortOptions(sortField, sortDirection);
            var paginationOptions = new PaginationOptions(pageSize, pageNumber);
            OrderFilterOptions? filterOptions = null;
            if (
                periodStart.HasValue
                || periodEnd.HasValue
                || status.HasValue
                || sellerId.HasValue
                || buyerId.HasValue
            )
            {
                filterOptions = new(periodStart, periodEnd, status, sellerId, buyerId);
            }
            return new(sortOptions, paginationOptions, filterOptions);
        }
    }
}
