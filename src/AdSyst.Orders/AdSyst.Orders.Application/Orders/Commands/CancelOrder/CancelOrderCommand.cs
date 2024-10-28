using MediatR;
using AdSyst.Orders.DAL.MongoDb.Enums;

namespace AdSyst.Orders.Application.Orders.Commands.CancelOrder
{
    public record CancelOrderCommand(Guid UserId, string OrderId) : IRequest<OrderStatus>;
}
