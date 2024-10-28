using Microsoft.Extensions.Logging;

namespace AdSyst.Moderation.Application.Advertisments.Events.AdvertismentCreated
{
    public static partial class AdvertismentCreatedEventLogs
    {
        [LoggerMessage(
            Level = LogLevel.Information,
            Message = "An event about creating advertisment {AdvertismentId} was received.",
            EventId = 5040101,
            EventName = "AdvertismentCreatedEventConsumed"
        )]
        public static partial void AdvertismentCreatedEventConsumedEventLog(
            this ILogger logger,
            Guid advertismentId
        );

        [LoggerMessage(
            Level = LogLevel.Information,
            Message = "The Advertisment {AdvertismentId} has been added to the system",
            EventId = 5040201,
            EventName = "AdvertismentAdded"
        )]
        public static partial void AdvertismentAddedEventLog(
            this ILogger logger,
            Guid advertismentId
        );

        [LoggerMessage(
            Level = LogLevel.Warning,
            Message = "Advertisment {AdvertismentId} by user {UserId} was not added. Exception: {ExceptionDetails}",
            EventId = 5040202,
            EventName = "AdvertismentAlreadyExists"
        )]
        public static partial void AdvertismentAlreadyExistsEventLog(
            this ILogger logger,
            Guid advertismentId,
            Guid userId,
            string exceptionDetails
        );
    }
}
