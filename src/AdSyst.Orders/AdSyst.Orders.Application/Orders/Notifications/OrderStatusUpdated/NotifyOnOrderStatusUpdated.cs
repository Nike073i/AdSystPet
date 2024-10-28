using MassTransit;
using MediatR;
using AdSyst.Common.Contracts.Orders;

namespace AdSyst.Orders.Application.Orders.Notifications.OrderStatusUpdated
{
    public class NotifyOnOrderStatusUpdated : INotificationHandler<OrderStatusUpdatedNotification>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public NotifyOnOrderStatusUpdated(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task Handle(
            OrderStatusUpdatedNotification notification,
            CancellationToken cancellationToken
        )
        {
            var statusUpdatedEvent = new OrderStatusUpdatedEvent
            {
                BuyerId = notification.BuyerId,
                SellerId = notification.SellerId,
                OrderId = notification.OrderId,
                NewStatus = notification.NewStatus.ToString(),
            };
            return _publishEndpoint.Publish(statusUpdatedEvent, cancellationToken);
        }
    }
}
