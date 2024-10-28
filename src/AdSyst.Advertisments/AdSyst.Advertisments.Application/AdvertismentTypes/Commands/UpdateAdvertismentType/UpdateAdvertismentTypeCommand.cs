using ErrorOr;
using MediatR;

namespace AdSyst.Advertisments.Application.AdvertismentTypes.Commands.UpdateAdvertismentType
{
    public record UpdateAdvertismentTypeCommand(Guid Id, string Name) : IRequest<ErrorOr<Guid>>;
}
