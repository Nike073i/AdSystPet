using MediatR;

namespace AdSyst.Moderation.Application.CorrectionNotes.Notifications.CorrectionNoteAdded
{
    public record CorrectionNoteAddedNotification(
        Guid AdvertismentId,
        string NoteId,
        string NoteTitle
    ) : INotification;
}
