using AdSyst.Orders.DAL.MongoDb.Enums;

namespace AdSyst.Orders.BusinessLayer.Extensions.Filter
{
    public record OrderFilterOptions(
        DateTimeOffset? PeriodStart = null,
        DateTimeOffset? PeriodEnd = null,
        OrderStatus? Status = null,
        Guid? SellerId = null,
        Guid? BuyerId = null
    );
}
