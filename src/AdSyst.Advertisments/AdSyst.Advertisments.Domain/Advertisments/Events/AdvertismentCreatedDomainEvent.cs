using AdSyst.Advertisments.Domain.Abstractions;

namespace AdSyst.Advertisments.Domain.Advertisments.Events
{
    public record AdvertismentCreatedDomainEvent(
        Guid UserId,
        Guid AdvertismentId,
        string Title,
        AdvertismentStatus Status
    ) : DomainEvent;
}
