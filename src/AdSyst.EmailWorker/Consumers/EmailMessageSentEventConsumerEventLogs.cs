namespace AdSyst.EmailWorker.Consumers
{
    public static partial class EmailMessageSentEventConsumerEventLogs
    {
        [LoggerMessage(
            LogLevel.Information,
            Message = "The message send event was consumed",
            EventId = 4020101,
            EventName = "EmailMessageSentEventConsumed"
        )]
        public static partial void EmailMessageSentEventConsumedEventLog(this ILogger logger);
    }
}
