using MediatR;
using AdSyst.Orders.DAL.MongoDb.Enums;

namespace AdSyst.Orders.Application.Orders.Notifications.OrderStatusUpdated
{
    public record OrderStatusUpdatedNotification(
        string OrderId,
        OrderStatus NewStatus,
        Guid BuyerId,
        Guid SellerId
    ) : INotification;
}
