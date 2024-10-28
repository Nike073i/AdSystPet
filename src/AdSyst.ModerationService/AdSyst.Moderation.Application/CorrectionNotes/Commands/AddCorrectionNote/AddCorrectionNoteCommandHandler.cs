using ErrorOr;
using MediatR;
using AdSyst.Moderation.Application.CorrectionNotes.Notifications.CorrectionNoteAdded;
using AdSyst.Moderation.BusinessLayer.Interfaces;
using AdSyst.Moderation.BusinessLayer.Models;

namespace AdSyst.Moderation.Application.CorrectionNotes.Commands.AddCorrectionNote
{
    public class AddCorrectionNoteCommandHandler
        : IRequestHandler<AddCorrectionNoteCommand, ErrorOr<string>>
    {
        private readonly IMediator _mediator;
        private readonly ICorrectionService _correctionService;

        public AddCorrectionNoteCommandHandler(
            IMediator mediator,
            ICorrectionService correctionService
        )
        {
            _mediator = mediator;
            _correctionService = correctionService;
        }

        public async Task<ErrorOr<string>> Handle(
            AddCorrectionNoteCommand request,
            CancellationToken cancellationToken
        )
        {
            var createDto = new CreateCorrectionNoteDto(
                request.Title,
                request.Description,
                request.ModeratorId,
                request.AdvertismentId
            );
            var executeResult = await _correctionService.AddCorrectionNoteAsync(
                createDto,
                cancellationToken
            );
            if (executeResult.IsError)
                return executeResult.Errors;

            string noteId = executeResult.Value;

            await _mediator.Publish(
                new CorrectionNoteAddedNotification(request.AdvertismentId, noteId, request.Title),
                cancellationToken
            );

            return noteId;
        }
    }
}
