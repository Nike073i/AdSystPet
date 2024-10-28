using Microsoft.Extensions.Logging;
using AdSyst.Common.Contracts.Orders;
using AdSyst.Notifications.Application.Enums;
using AdSyst.Notifications.Application.Services.Templates;
using AdSyst.Notifications.Application.Shared;
using AdSyst.Notifications.BusinessLayer.Interfaces;

namespace AdSyst.Notifications.Application.Orders.OrderStatusUpdated
{
    public class OrderStatusUpdatedEventNotifyBuyerConsumer : UserNotifierConsumer<OrderStatusUpdatedEvent>
    {
        public OrderStatusUpdatedEventNotifyBuyerConsumer(
            ILogger<OrderStatusUpdatedEventNotifyBuyerConsumer> logger,
            INotifyManager notifyManager,
            INotifyMessageManager messageManager
        ) : base(logger, notifyManager, messageManager) { }

        protected override NotificationEvent NotificationEvent => NotificationEvent.OrderStatusUpdated;

        protected override object GetMessageData(OrderStatusUpdatedEvent eventModel) =>
            new { eventModel.OrderId, eventModel.NewStatus };
        protected override Guid GetReceiverId(OrderStatusUpdatedEvent eventModel) => eventModel.BuyerId;
    }
}
