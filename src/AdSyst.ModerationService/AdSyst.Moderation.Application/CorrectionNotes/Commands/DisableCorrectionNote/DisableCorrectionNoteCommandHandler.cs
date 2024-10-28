using ErrorOr;
using MediatR;
using AdSyst.Moderation.BusinessLayer.Interfaces;

namespace AdSyst.Moderation.Application.CorrectionNotes.Commands.DisableCorrectionNote
{
    public class DisableCorrectionNoteCommandHandler
        : IRequestHandler<DisableCorrectionNoteCommand, ErrorOr<Success>>
    {
        private readonly ICorrectionService _correctionService;

        public DisableCorrectionNoteCommandHandler(ICorrectionService correctionService)
        {
            _correctionService = correctionService;
        }

        public Task<ErrorOr<Success>> Handle(
            DisableCorrectionNoteCommand request,
            CancellationToken cancellationToken
        ) =>
            _correctionService.DisableCorrectionNoteAsync(
                request.AdvertismentId,
                request.NoteId,
                cancellationToken
            );
    }
}
