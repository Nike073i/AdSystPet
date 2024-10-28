using ErrorOr;

namespace AdSyst.Advertisments.Domain.Advertisments.AdvertismentStates
{
    /// <summary>
    /// Объявление в статусе "В архиве"
    /// </summary>
    public class ArchivalState : IAdvertismentState
    {
        /// <inheritdoc />
        public AdvertismentStatus Status => AdvertismentStatus.Archival;

        /// <inheritdoc />
        public ErrorOr<IAdvertismentState> Approve() =>
            AdvertismentErrors.IncorrectStateChangeError(Status);

        /// <inheritdoc />
        public ErrorOr<IAdvertismentState> Archive() =>
            AdvertismentErrors.IncorrectStateChangeError(Status);

        /// <inheritdoc />
        public ErrorOr<IAdvertismentState> Reject() =>
            AdvertismentErrors.IncorrectStateChangeError(Status);

        /// <inheritdoc />
        public ErrorOr<IAdvertismentState> Restore() => new ModerationState();

        /// <inheritdoc />
        public ErrorOr<IAdvertismentState> Update() => this;
    }
}
