using AdSyst.Common.Contracts.Abstractions;

namespace AdSyst.Common.Contracts.Advertisments
{
    public class AdvertismentCreatedEvent : IIntegrationEvent
    {
        public Guid UserId { get; set; }
        public Guid AdvertismentId { get; set; }
        public string Title { get; set; } = null!;
        public string AdvertismentStatus { get; set; } = null!;
    }
}
