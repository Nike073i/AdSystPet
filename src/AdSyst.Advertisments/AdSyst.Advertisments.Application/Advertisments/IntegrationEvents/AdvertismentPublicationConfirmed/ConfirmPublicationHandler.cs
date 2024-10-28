using MediatR;
using Microsoft.Extensions.Logging;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Common.Application.Abstractions.Data;
using AdSyst.Common.Contracts.Moderation;

namespace AdSyst.Advertisments.Application.Advertisments.IntegrationEvents.AdvertismentPublicationConfirmed
{
    public class ConfirmPublicationHandler
        : INotificationHandler<AdvertismentPublicationConfirmedEvent>
    {
        private readonly ILogger<ConfirmPublicationHandler> _logger;
        private readonly IAdvertismentRepository _advertismentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmPublicationHandler(
            ILogger<ConfirmPublicationHandler> logger,
            IAdvertismentRepository advertismentRepository,
            IUnitOfWork unitOfWork
        )
        {
            _logger = logger;
            _advertismentRepository = advertismentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(
            AdvertismentPublicationConfirmedEvent notification,
            CancellationToken cancellationToken
        )
        {
            var advertismentId = notification.AdvertismentId;
            _logger.AdvertismentPublicationConfirmedEventConsumedEventLog(advertismentId);
            var advertisment = await _advertismentRepository.GetByIdAsync(
                advertismentId,
                cancellationToken: cancellationToken
            );
            if (advertisment is null)
            {
                _logger.AdvertismentNotFoundEventLog(advertismentId);
                return;
            }

            var approveResult = advertisment.Approve();
            if (approveResult.IsError)
            {
                _logger.AdvertismentWasNotConfirmedDueToIncorrectStateChangingEventLog(
                    advertismentId,
                    approveResult.FirstError.Description
                );
                return;
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.AdvertismentWasConfirmedEventLog(advertismentId);
        }
    }
}
