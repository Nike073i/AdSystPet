using AdSyst.Common.Contracts.Abstractions;

namespace AdSyst.Common.Contracts.Advertisments
{
    public class AdvertismentStatusChangedEvent : IIntegrationEvent
    {
        public Guid UserId { get; set; }
        public Guid AdvertismentId { get; set; }
        public string AdvertismentStatus { get; set; } = null!;
    }
}
