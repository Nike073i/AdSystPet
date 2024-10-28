using ErrorOr;
using MediatR;
using AdSyst.Moderation.Application.Advertisments.Notifications.AdvertismentPublicationConfirmed;
using AdSyst.Moderation.BusinessLayer.Interfaces;
using AdSyst.Moderation.DAL.MongoDb.Errors;

namespace AdSyst.Moderation.Application.Advertisments.Commands.ConfirmPublication
{
    public class ConfirmPublicationCommandHandler
        : IRequestHandler<ConfirmPublicationCommand, ErrorOr<Success>>
    {
        private readonly ICorrectionService _correctionService;
        private readonly IMediator _mediator;

        public ConfirmPublicationCommandHandler(
            ICorrectionService correctionService,
            IMediator mediator
        )
        {
            _correctionService = correctionService;
            _mediator = mediator;
        }

        public async Task<ErrorOr<Success>> Handle(
            ConfirmPublicationCommand request,
            CancellationToken cancellationToken
        )
        {
            var getResult = await _correctionService.GetAdvertismentAsync(
                request.AdvertismentId,
                cancellationToken
            );
            if (getResult.IsError)
                return getResult.Errors;
            var advertisment = getResult.Value;

            if (advertisment.CorrectionNotes.Any(note => note.IsActive))
                return AdvertismentErrors.HasNotes;

            await _mediator.Publish(
                new AdvertismentPublicationConfirmedNotification(
                    advertisment.AdvertismentAuthorId,
                    advertisment.AdvertismentId
                ),
                cancellationToken
            );
            return Result.Success;
        }
    }
}
