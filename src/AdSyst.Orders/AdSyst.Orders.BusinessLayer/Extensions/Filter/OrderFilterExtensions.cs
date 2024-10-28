using MongoDB.Driver;
using AdSyst.Orders.DAL.MongoDb.Enums;
using AdSyst.Orders.DAL.MongoDb.Models;

namespace AdSyst.Orders.BusinessLayer.Extensions.Filter
{
    public static class OrderFilterExtensions
    {
        public static FilterDefinition<Order> FilterByOptions(
            this FilterDefinitionBuilder<Order> builder,
            OrderFilterOptions? filterOptions
        ) =>
            filterOptions == null
                ? builder.Empty
                : builder.And(
                    FilterByBuyerId(builder, filterOptions.BuyerId),
                    FilterBySellerId(builder, filterOptions.SellerId),
                    FilterByStatus(builder, filterOptions.Status),
                    FilterByDate(builder, filterOptions.PeriodStart, filterOptions.PeriodEnd)
                );

        private static FilterDefinition<Order> FilterByDate(
            FilterDefinitionBuilder<Order> builder,
            DateTimeOffset? periodStart,
            DateTimeOffset? periodEnd
        )
        {
            var filterConstraint = new List<FilterDefinition<Order>>(2);
            if (periodStart.HasValue)
                filterConstraint.Add(builder.Gte(o => o.CreatedAt, periodStart));

            if (periodEnd.HasValue)
                filterConstraint.Add(builder.Lte(o => o.CreatedAt, periodEnd));

            return builder.And(filterConstraint);
        }

        private static FilterDefinition<Order> FilterBySellerId(
            FilterDefinitionBuilder<Order> builder,
            Guid? sellerId
        ) => sellerId.HasValue ? builder.Eq(o => o.SellerId, sellerId) : builder.Empty;

        private static FilterDefinition<Order> FilterByBuyerId(
            FilterDefinitionBuilder<Order> builder,
            Guid? buyerId
        ) => buyerId.HasValue ? builder.Eq(o => o.BuyerId, buyerId) : builder.Empty;

        private static FilterDefinition<Order> FilterByStatus(
            FilterDefinitionBuilder<Order> builder,
            OrderStatus? orderStatus
        ) => orderStatus.HasValue ? builder.Eq(o => o.Status, orderStatus) : builder.Empty;
    }
}
