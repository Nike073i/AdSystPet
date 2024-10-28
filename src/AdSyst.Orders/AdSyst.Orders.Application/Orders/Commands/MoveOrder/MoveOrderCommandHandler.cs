using MediatR;
using AdSyst.Orders.Application.Orders.Notifications.OrderStatusUpdated;
using AdSyst.Orders.BusinessLayer.Interfaces;
using AdSyst.Orders.DAL.MongoDb.Enums;

namespace AdSyst.Orders.Application.Orders.Commands.MoveOrder
{
    public class MoveOrderCommandHandler : IRequestHandler<MoveOrderCommand, OrderStatus>
    {
        private readonly IMediator _mediator;
        private readonly IOrderService _orderService;

        public MoveOrderCommandHandler(IMediator mediator, IOrderService service)
        {
            _mediator = mediator;
            _orderService = service;
        }

        public async Task<OrderStatus> Handle(
            MoveOrderCommand request,
            CancellationToken cancellationToken
        )
        {
            string orderId = request.OrderId;

            var order = await _orderService.MoveStatusAsync(orderId, cancellationToken);
            await _mediator.Publish(
                new OrderStatusUpdatedNotification(
                    orderId,
                    order.Status,
                    order.BuyerId,
                    order.SellerId
                ),
                cancellationToken
            );
            return order.Status;
        }
    }
}
