using MediatR;

namespace AdSyst.Orders.Application.Orders.Queries.GetOrderSystemData
{
    public record GetOrderSystemDataQuery(string OrderId)
        : IRequest<GetOrderSystemDataQueryResponse>;
}
