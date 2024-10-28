using MediatR;

namespace AdSyst.Moderation.Application.Advertisments.Notifications.AdvertismentPublicationConfirmed
{
    public record AdvertismentPublicationConfirmedNotification(Guid UserId, Guid AdvertismentId)
        : INotification;
}
