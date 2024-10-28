using MediatR;
using AdSyst.Orders.DAL.MongoDb.Enums;

namespace AdSyst.Orders.Application.Orders.Commands.MoveOrder
{
    public record MoveOrderCommand(Guid UserId, string OrderId) : IRequest<OrderStatus>;
}
