using ErrorOr;
using MediatR;

namespace AdSyst.Moderation.Application.Advertisments.Queries.GetAdvertismentSystemData
{
    public record GetAdvertismentSystemDataQuery(Guid AdvertismentId)
        : IRequest<ErrorOr<AdvertismentSystemData>>;
}
