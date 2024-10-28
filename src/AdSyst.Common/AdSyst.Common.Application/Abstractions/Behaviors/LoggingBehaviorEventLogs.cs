using Microsoft.Extensions.Logging;

namespace AdSyst.Common.Application.Abstractions.Behaviors
{
    internal static partial class LoggingBehaviorEventLogs
    {
        [LoggerMessage(
            Level = LogLevel.Information,
            Message = "Request {RequestName} recevied",
            EventId = 9999001,
            EventName = "LoggingBehavior.RequestReceived"
        )]
        public static partial void RequestReceived(this ILogger logger, string requestName);

        [LoggerMessage(
            Level = LogLevel.Warning,
            Message = "Request {RequestName} processed with error",
            EventId = 9999002,
            EventName = "LoggingBehavior.RequestCompletedWithError"
        )]
        public static partial void RequestCompletedWithError(
            this ILogger logger,
            string requestName
        );

        [LoggerMessage(
            Level = LogLevel.Information,
            Message = "Request {RequestName} processed successfully",
            EventId = 9999003,
            EventName = "LoggingBehavior.RequestCompletedSuccessfully"
        )]
        public static partial void RequestCompletedSuccessfully(
            this ILogger logger,
            string requestName
        );

        [LoggerMessage(
            Level = LogLevel.Error,
            Message = "Request {RequestName} processing failed",
            EventId = 9999004,
            EventName = "LoggingBehavior.RequestFailed"
        )]
        public static partial void RequestFailed(
            this ILogger logger,
            string requestName,
            Exception? exception = null
        );
    }
}
