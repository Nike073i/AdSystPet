using Microsoft.Extensions.Logging;

namespace AdSyst.Common.Presentation.Middlewares.OperationCanceled
{
    public static partial class OperationCanceledMiddlewareEventLogs
    {
        [LoggerMessage(
            EventId = 9990101,
            EventName = "OperationWasCanceled",
            Level = LogLevel.Information,
            Message = "Request with TraceId = {TraceId} was canceled"
        )]
        public static partial void OperationWasCanceledEventLog(
            this ILogger logger,
            string traceId
        );
    }
}
