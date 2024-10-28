using AdSyst.Orders.DAL.MongoDb.Enums;
using AdSyst.Orders.DAL.MongoDb.Models;

namespace AdSyst.Orders.Application.Orders.Queries.GetOrderDetails
{
    public record OrderDetailsViewModel(
        string Id,
        decimal Price,
        DateTimeOffset CreatedAt,
        OrderStatus Status,
        string? TrackNumber,
        Guid SellerId,
        Guid BuyerId,
        int? AdvertismentId,
        Advertisment? Advertisment,
        Address Address
    );
}
