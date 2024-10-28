using Microsoft.Extensions.Logging;
using AdSyst.Common.Contracts.Orders;
using AdSyst.Notifications.Application.Enums;
using AdSyst.Notifications.Application.Services.Templates;
using AdSyst.Notifications.Application.Shared;
using AdSyst.Notifications.BusinessLayer.Interfaces;

namespace AdSyst.Notifications.Application.Orders.OrderCanceled
{
    public class OrderCanceledEventNotifyBuyerConsumer : UserNotifierConsumer<OrderCanceledEvent>
    {
        public OrderCanceledEventNotifyBuyerConsumer(
            ILogger<OrderCanceledEventNotifyBuyerConsumer> logger,
            INotifyManager notifyManager,
            INotifyMessageManager messageManager
        ) : base(logger, notifyManager, messageManager) { }

        protected override NotificationEvent NotificationEvent => NotificationEvent.OrderCanceled;

        protected override object GetMessageData(OrderCanceledEvent eventModel) =>
            new { eventModel.OrderId, eventModel.NewStatus };
        protected override Guid GetReceiverId(OrderCanceledEvent eventModel) => eventModel.BuyerId;
    }
}
