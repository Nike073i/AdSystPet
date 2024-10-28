using MediatR;
using AdSyst.Orders.Application.Orders.Notifications.OrderCanceled;
using AdSyst.Orders.BusinessLayer.Interfaces;
using AdSyst.Orders.DAL.MongoDb.Enums;

namespace AdSyst.Orders.Application.Orders.Commands.CancelOrder
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, OrderStatus>
    {
        private readonly IMediator _mediator;
        private readonly IOrderService _orderService;

        public CancelOrderCommandHandler(IMediator mediator, IOrderService service)
        {
            _mediator = mediator;
            _orderService = service;
        }

        public async Task<OrderStatus> Handle(
            CancelOrderCommand request,
            CancellationToken cancellationToken
        )
        {
            string orderId = request.OrderId;

            var order = await _orderService.CancelAsync(orderId, cancellationToken);
            await _mediator.Publish(
                new OrderCanceledNotification(orderId, order.BuyerId, order.SellerId),
                cancellationToken
            );
            return order.Status;
        }
    }
}
