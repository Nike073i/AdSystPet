using ErrorOr;
using MediatR;
using AdSyst.Advertisments.Domain.Advertisments;

namespace AdSyst.Advertisments.Application.Advertisments.Commands.ArchiveAdvertisment
{
    public record ArchiveAdvertismentCommand(Guid AdvertismentId)
        : IRequest<ErrorOr<AdvertismentStatus>>;
}
