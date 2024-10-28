using Microsoft.Extensions.Logging;

namespace AdSyst.AuthService.Application.Users.DomainEvents.UserCreated
{
    public static partial class SendEmailConfirmationMessageLogs
    {
        [LoggerMessage(
            Level = LogLevel.Warning,
            Message = "User ({UserId}) is not found",
            EventId = 2020101,
            EventName = "UserNotFound"
        )]
        public static partial void UserNotFound(this ILogger logger, string userId);
    }
}
