using AdSyst.Common.Contracts.Abstractions;

namespace AdSyst.Common.Contracts.Moderation
{
    public class AdvertismentPublicationConfirmedEvent : IIntegrationEvent
    {
        public Guid UserId { get; set; }
        public Guid AdvertismentId { get; set; }
    }
}
