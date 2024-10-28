using MassTransit;
using MediatR;
using MongoDB.Driver;
using AdSyst.Common.Contracts.Moderation;
using AdSyst.Moderation.DAL.MongoDb.Models;

namespace AdSyst.Moderation.Application.CorrectionNotes.Notifications.CorrectionNoteAdded
{
    public class NotifyUserOnCorrectionNoteAdded
        : INotificationHandler<CorrectionNoteAddedNotification>
    {
        private readonly IMongoCollection<Advertisment> _advertisments;
        private readonly IPublishEndpoint _publishEndpoint;

        public NotifyUserOnCorrectionNoteAdded(
            IMongoCollection<Advertisment> advertisments,
            IPublishEndpoint publishEndpoint
        )
        {
            _advertisments = advertisments;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(
            CorrectionNoteAddedNotification notification,
            CancellationToken cancellationToken
        )
        {
            var advertismentAuthorId = await _advertisments
                .Find(ad => ad.AdvertismentId == notification.AdvertismentId)
                .Project(ad => ad.AdvertismentAuthorId)
                .FirstOrDefaultAsync(cancellationToken);
            if (advertismentAuthorId == Guid.Empty)
                return;

            var noteAddedEvent = new CorrectionNoteAddedEvent
            {
                AdvertismentId = notification.AdvertismentId,
                NoteTitle = notification.NoteTitle,
                AdvertismentAuthorId = advertismentAuthorId,
            };
            await _publishEndpoint.Publish(noteAddedEvent, cancellationToken);
        }
    }
}
