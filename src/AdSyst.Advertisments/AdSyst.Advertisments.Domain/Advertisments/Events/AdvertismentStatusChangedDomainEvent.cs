using AdSyst.Advertisments.Domain.Abstractions;

namespace AdSyst.Advertisments.Domain.Advertisments.Events
{
    public record AdvertismentStatusChangedDomainEvent(
        Guid UserId,
        Guid AdvertismentId,
        AdvertismentStatus AdvertismentStatus
    ) : DomainEvent;
}
