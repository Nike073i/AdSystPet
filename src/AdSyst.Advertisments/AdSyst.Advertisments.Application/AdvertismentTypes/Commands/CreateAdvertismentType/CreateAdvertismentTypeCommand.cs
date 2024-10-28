using ErrorOr;
using MediatR;

namespace AdSyst.Advertisments.Application.AdvertismentTypes.Commands.CreateAdvertismentType
{
    public record CreateAdvertismentTypeCommand(string Name) : IRequest<ErrorOr<Guid>>;
}
