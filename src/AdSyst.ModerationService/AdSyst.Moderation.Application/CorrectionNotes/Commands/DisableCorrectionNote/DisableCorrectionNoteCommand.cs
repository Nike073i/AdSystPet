using ErrorOr;
using MediatR;

namespace AdSyst.Moderation.Application.CorrectionNotes.Commands.DisableCorrectionNote
{
    public record DisableCorrectionNoteCommand(Guid AdvertismentId, string NoteId)
        : IRequest<ErrorOr<Success>>;
}
