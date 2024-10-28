using Microsoft.Extensions.Logging;

namespace AdSyst.Advertisments.Application.Advertisments.IntegrationEvents.CorrectionNoteAdded
{
    public static partial class CorrectionNoteAddedHandlerEventLogs
    {
        [LoggerMessage(
            Level = LogLevel.Information,
            Message = "An event about adding a correction note to advertisment {AdvertismentId} was received",
            EventId = 1090101,
            EventName = "CorrectionNoteAddedEventConsumed"
        )]
        public static partial void CorrectionNoteAddedEventConsumedEventLog(
            this ILogger logger,
            Guid advertismentId
        );

        [LoggerMessage(
            Level = LogLevel.Information,
            Message = "An advertisment {AdvertismentId} was rejected",
            EventId = 1090201,
            EventName = "AdvertismentWasRejected"
        )]
        public static partial void AdvertismentWasRejectedEventLog(
            this ILogger logger,
            Guid advertismentId
        );

        [LoggerMessage(
            Level = LogLevel.Warning,
            Message = "The correction note {NoteTitle} was added to an unknown advertisment {AdvertismentId}",
            EventId = 1090202,
            EventName = "CorrectionNoteWasAddedToUnknownAdvertisment"
        )]
        public static partial void CorrectionNoteWasAddedToUnknownAdvertismentEventLog(
            this ILogger logger,
            string noteTitle,
            Guid advertismentId
        );

        [LoggerMessage(
            Level = LogLevel.Error,
            Message = "The correction note \"{NoteTitle}\" was added to the advertisment {AdvertismentId}, but there was a exception: {ExceptionDetails}",
            EventId = 1090203,
            EventName = "AdvertismentWasNotRejectedDueToIncorrectStateChanging"
        )]
        public static partial void AdvertismentWasNotRejectedDueToIncorrectStateChangingEventLog(
            this ILogger logger,
            Guid advertismentId,
            string noteTitle,
            string exceptionDetails
        );
    }
}
