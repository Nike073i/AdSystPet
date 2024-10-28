using ErrorOr;
using MediatR;

namespace AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentSystemData
{
    public record GetAdvertismentSystemDataQuery(Guid AdvertismentId)
        : IRequest<ErrorOr<AdvertismentSystemViewModel>>;
}
