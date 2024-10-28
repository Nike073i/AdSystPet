using AdSyst.Orders.DAL.MongoDb.Enums;

namespace AdSyst.Orders.Application.Orders.Queries.GetOrderSystemData
{
    public record OrderSystemViewModel(
        string Id,
        Guid SellerId,
        Guid BuyerId,
        OrderStatus OrderStatus,
        DateTimeOffset CreatedAt
    );
}
