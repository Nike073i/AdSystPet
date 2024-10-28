using ErrorOr;
using MediatR;

namespace AdSyst.Advertisments.Application.AdvertismentTypes.Queries.GetAdvertismentTypeById
{
    public record GetAdvertismentTypeByIdQuery(Guid Id)
        : IRequest<ErrorOr<AdvertismentTypeViewModel>>;
}
