using System.Linq.Expressions;
using MongoDB.Driver;
using AdSyst.Orders.BusinessLayer.Enums;
using AdSyst.Orders.DAL.MongoDb.Models;

namespace AdSyst.Orders.BusinessLayer.Extensions.Sorting
{
    public static class OrderSortExtensions
    {
        public static SortDefinition<Order> SortByOptions(
            this SortDefinitionBuilder<Order> builder,
            OrderSortOptions sortOptions
        )
        {
            Expression<Func<Order, object>> sortFieldSelector = sortOptions.SortField switch
            {
                SortOrderField.Id => o => o.Id,
                SortOrderField.Date => o => o.CreatedAt,
                SortOrderField.Price => o => o.Price,
                SortOrderField.Status => o => o.Status,
                _ => throw new InvalidOperationException(),
            };
            return sortOptions.SortDirection == Common.BusinessLayer.Enums.SortDirection.Asc
                ? builder.Ascending(sortFieldSelector)
                : builder.Descending(sortFieldSelector);
        }
    }
}
