using MediatR;

namespace AdSyst.Orders.Application.Advertisments.Queries.GetAdvertismentSystemData
{
    public record GetAdvertismentSystemDataQuery(Guid AdvertismentId)
        : IRequest<GetAdvertismentSystemDataQueryResponse>;
}
