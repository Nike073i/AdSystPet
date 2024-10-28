using MediatR;
using Microsoft.Extensions.Logging;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Common.Application.Abstractions.Data;
using AdSyst.Common.Contracts.Moderation;

namespace AdSyst.Advertisments.Application.Advertisments.IntegrationEvents.CorrectionNoteAdded
{
    public class CorrectionNoteAddedHandler : INotificationHandler<CorrectionNoteAddedEvent>
    {
        private readonly ILogger<CorrectionNoteAddedHandler> _logger;
        private readonly IAdvertismentRepository _advertismentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CorrectionNoteAddedHandler(
            ILogger<CorrectionNoteAddedHandler> logger,
            IAdvertismentRepository advertismentRepository,
            IUnitOfWork unitOfWork
        )
        {
            _logger = logger;
            _advertismentRepository = advertismentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(
            CorrectionNoteAddedEvent notification,
            CancellationToken cancellationToken
        )
        {
            var advertismentId = notification.AdvertismentId;

            _logger.CorrectionNoteAddedEventConsumedEventLog(advertismentId);

            var advertisment = await _advertismentRepository.GetByIdAsync(
                advertismentId,
                cancellationToken: cancellationToken
            );
            if (advertisment is null)
            {
                _logger.CorrectionNoteWasAddedToUnknownAdvertismentEventLog(
                    notification.NoteTitle,
                    advertismentId
                );
                return;
            }

            if (advertisment.Status == AdvertismentStatus.Rejected)
                return;

            var rejectResult = advertisment.Reject();

            if (rejectResult.IsError)
            {
                _logger.AdvertismentWasNotRejectedDueToIncorrectStateChangingEventLog(
                    advertismentId,
                    notification.NoteTitle,
                    rejectResult.FirstError.Description
                );
                return;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.AdvertismentWasRejectedEventLog(advertismentId);
        }
    }
}
