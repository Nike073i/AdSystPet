using ErrorOr;

namespace AdSyst.Advertisments.Domain.Advertisments.AdvertismentStates
{
    /// <summary>
    /// Объявление в статусе "На модерации"
    /// </summary>
    public class ModerationState : IAdvertismentState
    {
        /// <inheritdoc />
        public AdvertismentStatus Status => AdvertismentStatus.Moderation;

        /// <inheritdoc />
        public ErrorOr<IAdvertismentState> Approve() => new ActiveState();

        /// <inheritdoc />
        public ErrorOr<IAdvertismentState> Archive() => new ArchivalState();

        /// <inheritdoc />
        public ErrorOr<IAdvertismentState> Reject() => new RejectedState();

        /// <inheritdoc />
        public ErrorOr<IAdvertismentState> Restore() =>
            AdvertismentErrors.IncorrectStateChangeError(Status);

        /// <inheritdoc />
        public ErrorOr<IAdvertismentState> Update() => this;
    }
}
