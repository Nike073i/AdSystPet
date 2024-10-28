using AdSyst.Orders.DAL.MongoDb.Enums;
using AdSyst.Orders.DAL.MongoDb.Models;

namespace AdSyst.Orders.Application.Orders.Queries.GetOrderList
{
    public record OrderViewModel(
        string Id,
        decimal Price,
        DateTimeOffset CreatedAt,
        OrderStatus Status,
        int? AdvertismentId,
        Advertisment? Advertisment
    );
}
