using ErrorOr;

namespace AdSyst.Advertisments.Domain.Advertisments.AdvertismentStates
{
    /// <summary>
    /// Объявление в статусе "Отклонено"
    /// </summary>
    public class RejectedState : IAdvertismentState
    {
        /// <inheritdoc />
        public AdvertismentStatus Status => AdvertismentStatus.Rejected;

        /// <inheritdoc />
        public ErrorOr<IAdvertismentState> Approve() =>
            AdvertismentErrors.IncorrectStateChangeError(Status);

        /// <inheritdoc />
        public ErrorOr<IAdvertismentState> Archive() => new ArchivalState();

        /// <inheritdoc />
        public ErrorOr<IAdvertismentState> Reject() =>
            AdvertismentErrors.IncorrectStateChangeError(Status);

        /// <inheritdoc />
        public ErrorOr<IAdvertismentState> Restore() =>
            AdvertismentErrors.IncorrectStateChangeError(Status);

        /// <inheritdoc />
        public ErrorOr<IAdvertismentState> Update() => new ModerationState();
    }
}
