using Microsoft.Extensions.Logging;

namespace AdSyst.Advertisments.Application.Advertisments.IntegrationEvents.AdvertismentPublicationConfirmed
{
    public static partial class ConfirmPublicationHandlerEventLogs
    {
        [LoggerMessage(
            Level = LogLevel.Information,
            Message = "An event confirming the publication of advertisement {AdvertismentId} was received",
            EventId = 1100101,
            EventName = "AdvertismentPublicationConfirmedEventConsumed"
        )]
        public static partial void AdvertismentPublicationConfirmedEventConsumedEventLog(
            this ILogger logger,
            Guid advertismentId
        );

        [LoggerMessage(
            Level = LogLevel.Information,
            Message = "The advertisment {AdvertismentId} was published",
            EventId = 1100201,
            EventName = "AdvertismentWasConfirmed"
        )]
        public static partial void AdvertismentWasConfirmedEventLog(
            this ILogger logger,
            Guid advertismentId
        );

        [LoggerMessage(
            Level = LogLevel.Warning,
            Message = "An event about the publication of advertisment {AdvertismentId} was received, but it was not found",
            EventId = 1100202,
            EventName = "AdvertismentNotFound"
        )]
        public static partial void AdvertismentNotFoundEventLog(
            this ILogger logger,
            Guid advertismentId
        );

        [LoggerMessage(
            Level = LogLevel.Error,
            Message = "The advertisment {AdvertismentId} was not published. Exception: {ExceptionDetails}",
            EventId = 1100203,
            EventName = "AdvertismentWasNotRejectedDueToIncorrectStateChanging"
        )]
        public static partial void AdvertismentWasNotConfirmedDueToIncorrectStateChangingEventLog(
            this ILogger logger,
            Guid advertismentId,
            string exceptionDetails
        );
    }
}
