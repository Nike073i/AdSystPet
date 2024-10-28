using MassTransit;
using MediatR;
using AdSyst.Common.Contracts.Orders;
using AdSyst.Orders.DAL.MongoDb.Enums;

namespace AdSyst.Orders.Application.Orders.Notifications.OrderCanceled
{
    public class NotifyOnOrderCanceled : INotificationHandler<OrderCanceledNotification>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public NotifyOnOrderCanceled(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task Handle(
            OrderCanceledNotification notification,
            CancellationToken cancellationToken
        )
        {
            var orderCanceledEvent = new OrderCanceledEvent
            {
                BuyerId = notification.BuyerId,
                SellerId = notification.SellerId,
                OrderId = notification.OrderId,
                NewStatus = OrderStatus.Canceled.ToString(),
            };
            return _publishEndpoint.Publish(orderCanceledEvent, cancellationToken);
        }
    }
}
