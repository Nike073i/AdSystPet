using MassTransit;
using MediatR;
using AdSyst.Common.Contracts.Orders;
using AdSyst.Orders.DAL.MongoDb.Enums;

namespace AdSyst.Orders.Application.Orders.Notifications.OrderCreated
{
    public class NotifyOnOrderCreated : INotificationHandler<OrderCreatedNotification>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public NotifyOnOrderCreated(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task Handle(
            OrderCreatedNotification notification,
            CancellationToken cancellationToken
        )
        {
            var orderCreatedEvent = new OrderCreatedEvent
            {
                BuyerId = notification.BuyerId,
                SellerId = notification.SellerId,
                OrderId = notification.OrderId,
                NewStatus = OrderStatus.Created.ToString(),
            };
            return _publishEndpoint.Publish(orderCreatedEvent, cancellationToken);
        }
    }
}
