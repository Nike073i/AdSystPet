using AdSyst.Common.Contracts.Abstractions;

namespace AdSyst.Common.Contracts.Moderation
{
    public class CorrectionNoteAddedEvent : IIntegrationEvent
    {
        public Guid AdvertismentAuthorId { get; set; }
        public Guid AdvertismentId { get; set; }
        public string NoteTitle { get; set; } = null!;
    }
}
