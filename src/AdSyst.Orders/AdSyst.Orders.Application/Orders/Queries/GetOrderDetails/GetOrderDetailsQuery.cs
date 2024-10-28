using MediatR;

namespace AdSyst.Orders.Application.Orders.Queries.GetOrderDetails
{
    public record GetOrderDetailsQuery(string Id, Guid UserId)
        : IRequest<GetOrderDetailsQueryResponse>;
}
