using Microsoft.Extensions.Logging;
using AdSyst.Common.Contracts.Orders;
using AdSyst.Notifications.Application.Enums;
using AdSyst.Notifications.Application.Services.Templates;
using AdSyst.Notifications.Application.Shared;
using AdSyst.Notifications.BusinessLayer.Interfaces;

namespace AdSyst.Notifications.Application.Orders.OrderCreated
{
    public class OrderCreatedEventNotifyBuyerConsumer : UserNotifierConsumer<OrderCreatedEvent>
    {
        public OrderCreatedEventNotifyBuyerConsumer(
            ILogger<OrderCreatedEventNotifyBuyerConsumer> logger,
            INotifyManager notifyManager,
            INotifyMessageManager messageManager
        ) : base(logger, notifyManager, messageManager) { }

        protected override NotificationEvent NotificationEvent => NotificationEvent.OrderCreated;

        protected override object GetMessageData(OrderCreatedEvent eventModel) =>
            new { eventModel.OrderId, eventModel.NewStatus };
        protected override Guid GetReceiverId(OrderCreatedEvent eventModel) => eventModel.BuyerId;
    }
}
