namespace AdSyst.EmailWorker.Jobs
{
    public static partial class EmailSendJobEventLogs
    {
        [LoggerMessage(
            LogLevel.Information,
            Message = "EmailSendJob started work",
            EventId = 4010101,
            EventName = "EmailSendJobStarted"
        )]
        public static partial void JobStartedEventLog(this ILogger logger);

        [LoggerMessage(
            LogLevel.Information,
            Message = "Found {Count} messages to send",
            EventId = 4010201,
            EventName = "MessagesFounded"
        )]
        public static partial void MessagesFoundedEventLog(this ILogger logger, int count);

        [LoggerMessage(
            LogLevel.Information,
            Message = "The iteration of sending letters begins",
            EventId = 4010301,
            EventName = "IterationStarted"
        )]
        public static partial void IterationStarted(this ILogger logger);

        [LoggerMessage(
            LogLevel.Information,
            Message = "Message sent",
            EventId = 4010401,
            EventName = "MessageSent"
        )]
        public static partial void MessageSentEventLog(this ILogger logger);

        [LoggerMessage(
            LogLevel.Information,
            Message = "A connection to the mail service has been established",
            EventId = 4010501,
            EventName = "ConnectionEstablished"
        )]
        public static partial void ConnectionЕstablishedEventLog(this ILogger logger);

        [LoggerMessage(
            LogLevel.Critical,
            Message = "The connection to the mail service was not established. Exception : {ExceptionDetails}",
            EventId = 4010502,
            EventName = "ConnectionWasNotEstablished"
        )]
        public static partial void ConnectionWasNotЕstablishedEventLog(
            this ILogger logger,
            string exceptionDetails
        );

        [LoggerMessage(
            LogLevel.Information,
            Message = "The iteration of sending letters ends",
            EventId = 4010601,
            EventName = "IterationFinished"
        )]
        public static partial void IterationFinished(this ILogger logger);
    }
}
