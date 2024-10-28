using MediatR;

namespace AdSyst.Orders.Application.Orders.Notifications.OrderCreated
{
    public record OrderCreatedNotification(string OrderId, Guid BuyerId, Guid SellerId)
        : INotification;
}
