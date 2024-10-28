using MediatR;
using AdSyst.Orders.DAL.MongoDb.Models;

namespace AdSyst.Orders.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(Guid AdvertismentId, Guid BuyerId, Address Address)
        : IRequest<string>;
}
