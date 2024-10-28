using ErrorOr;
using MediatR;

namespace AdSyst.Advertisments.Application.AdvertismentTypes.Queries.GetAdvertismentTypeList
{
    public record GetAdvertismentTypeListQuery()
        : IRequest<ErrorOr<IReadOnlyList<AdvertismentTypeViewModel>>>;
}
