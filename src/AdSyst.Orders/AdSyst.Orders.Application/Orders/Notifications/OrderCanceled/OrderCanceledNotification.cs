using MediatR;

namespace AdSyst.Orders.Application.Orders.Notifications.OrderCanceled
{
    public record OrderCanceledNotification(string OrderId, Guid BuyerId, Guid SellerId)
        : INotification;
}
