using MediatR;
using AdSyst.Orders.Application.Orders.Notifications.OrderCreated;
using AdSyst.Orders.BusinessLayer.Exceptions;
using AdSyst.Orders.BusinessLayer.Interfaces;
using AdSyst.Orders.DAL.MongoDb.Models;
using AdSyst.Orders.SyncDataServices.Advertisments.Interfaces;
using AdSyst.Orders.SyncDataServices.Advertisments.Models;

namespace AdSyst.Orders.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IAdvertismentServiceClient _advertismentService;
        private readonly IOrderService _orderService;

        public CreateOrderCommandHandler(
            IMediator mediator,
            IAdvertismentServiceClient advertismentService,
            IOrderService orderService
        )
        {
            _mediator = mediator;
            _advertismentService = advertismentService;
            _orderService = orderService;
        }

        public async Task<string> Handle(
            CreateOrderCommand request,
            CancellationToken cancellationToken
        )
        {
            (var advertismentId, var buyerId, var address) = request;
            var advertismentDetails = await _advertismentService.GetAdvertismentDetailsAsync(
                advertismentId,
                cancellationToken
            );

            if (advertismentDetails.Status != AdvertismentStatus.Active)
            {
                throw new OrderManagementException(
                    "Создать заказ",
                    "Нельзя создать заказ от неактивного объявления"
                );
            }

            var sellerId = advertismentDetails.UserId;
            var advertisment = new Advertisment(
                advertismentDetails.Id,
                advertismentDetails.Title,
                advertismentDetails.ImageIds.FirstOrDefault()
            );
            string orderId = await _orderService.CreateAsync(
                new(advertismentDetails.Price, sellerId, buyerId, advertisment, address),
                cancellationToken
            );

            await _mediator.Publish(
                new OrderCreatedNotification(orderId, buyerId, sellerId),
                cancellationToken
            );
            return orderId;
        }
    }
}
