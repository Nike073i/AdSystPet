using ErrorOr;

namespace AdSyst.Advertisments.Domain.Advertisments.AdvertismentStates
{
    /// <summary>
    /// Объявление в статусе "Активно"
    /// </summary>
    public class ActiveState : IAdvertismentState
    {
        /// <inheritdoc />
        public AdvertismentStatus Status => AdvertismentStatus.Active;

        /// <inheritdoc />
        public ErrorOr<IAdvertismentState> Approve() =>
            AdvertismentErrors.IncorrectStateChangeError(Status);

        /// <inheritdoc />
        public ErrorOr<IAdvertismentState> Archive() => new ArchivalState();

        /// <inheritdoc />
        public ErrorOr<IAdvertismentState> Reject() => new RejectedState();

        /// <inheritdoc />
        public ErrorOr<IAdvertismentState> Restore() =>
            AdvertismentErrors.IncorrectStateChangeError(Status);

        /// <inheritdoc />
        public ErrorOr<IAdvertismentState> Update() => new ModerationState();
    }
}
