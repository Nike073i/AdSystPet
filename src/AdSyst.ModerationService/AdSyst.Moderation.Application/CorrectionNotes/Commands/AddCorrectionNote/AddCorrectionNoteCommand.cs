using ErrorOr;
using MediatR;

namespace AdSyst.Moderation.Application.CorrectionNotes.Commands.AddCorrectionNote
{
    public record AddCorrectionNoteCommand(
        string Title,
        string? Description,
        Guid ModeratorId,
        Guid AdvertismentId
    ) : IRequest<ErrorOr<string>>;
}
