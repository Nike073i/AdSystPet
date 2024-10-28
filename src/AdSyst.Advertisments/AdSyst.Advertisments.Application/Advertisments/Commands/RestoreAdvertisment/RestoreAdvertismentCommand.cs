using ErrorOr;
using MediatR;
using AdSyst.Advertisments.Domain.Advertisments;

namespace AdSyst.Advertisments.Application.Advertisments.Commands.RestoreAdvertisment
{
    public record RestoreAdvertismentCommand(Guid AdvertismentId)
        : IRequest<ErrorOr<AdvertismentStatus>>;
}
