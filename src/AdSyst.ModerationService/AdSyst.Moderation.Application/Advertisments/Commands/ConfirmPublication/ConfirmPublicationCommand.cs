using ErrorOr;
using MediatR;

namespace AdSyst.Moderation.Application.Advertisments.Commands.ConfirmPublication
{
    public record ConfirmPublicationCommand(Guid AdvertismentId) : IRequest<ErrorOr<Success>>;
}
