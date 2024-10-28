using ErrorOr;
using MediatR;

namespace AdSyst.Moderation.Application.Advertisments.Commands.CreateAdvertisment
{
    public record CreateAdvertismentCommand(Guid AdvertismentId, Guid AdvertismentAuthorId)
        : IRequest<ErrorOr<Created>>;
}
