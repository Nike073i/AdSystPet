using AdSyst.Orders.Api.Models;

namespace AdSyst.Orders.Api.Logging.OrderEvents
{
    public static partial class OrderEventLogs
    {
        [LoggerMessage(
            LogLevel.Information,
            "A new order ({OrderId}) was created by {UserId}",
            EventId = 6010101,
            EventName = "OrderCreated"
        )]
        public static partial void OrderCreatedEventLog(
            this ILogger logger,
            string orderId,
            Guid userId
        );

        [LoggerMessage(
            LogLevel.Warning,
            "The order from the user ({UserId}) by advertisement {AdvertisementId} was not created because the advertisement was not found",
            EventId = 6010102,
            EventName = "OrderWasNotCreatedDueToResourceNotFound"
        )]
        public static partial void OrderWasNotCreatedDueToResourceNotFoundEventLog(
            this ILogger logger,
            Guid userId,
            Guid advertisementId
        );

        [LoggerMessage(
            LogLevel.Warning,
            "The order from the user ({UserId}) by advertisement {AdvertisementId} was not created. Exception: {ExceptionDetails}",
            EventId = 6010103,
            EventName = "OrderWasNotCreatedDueToBusinessLogicViolation"
        )]
        public static partial void OrderWasNotCreatedDueToBusinessLogicViolationEventLog(
            this ILogger logger,
            Guid userId,
            Guid advertisementId,
            string exceptionDetails
        );

        [LoggerMessage(
            LogLevel.Warning,
            "The order from the user ({UserId}) by advertisement {AdvertisementId} was not created. Exception: {ExceptionDetails}",
            EventId = 6010104,
            EventName = "OrderWasNotCreatedDueToAccessDenied"
        )]
        public static partial void OrderWasNotCreatedDueToAccessDeniedEventLog(
            this ILogger logger,
            Guid userId,
            Guid advertisementId,
            string exceptionDetails
        );

        [LoggerMessage(
            LogLevel.Information,
            "The order with Id = {OrderId} received",
            EventId = 6010201,
            EventName = "OrderReceived"
        )]
        public static partial void OrderReceivedEventLog(this ILogger logger, string orderId);

        [LoggerMessage(
            LogLevel.Warning,
            "The order with ID = {OrderId} was requested, but the resource was not found",
            EventId = 6010202,
            EventName = "OrderNotFound"
        )]
        public static partial void OrderNotFoundEventLog(this ILogger logger, string orderId);

        [LoggerMessage(
            LogLevel.Warning,
            "The order with ID = {OrderId} was requested by {UserId}, but was not received because the user does not have access to order. Exception: {ExceptionDetails}",
            EventId = 6010203,
            EventName = "OrderWasNotReceivedDueToAccessDenied"
        )]
        public static partial void OrderWasNotReceivedDueToAccessDeniedEventLog(
            this ILogger logger,
            string orderId,
            Guid userId,
            string exceptionDetails
        );

        [LoggerMessage(
            LogLevel.Information,
            "A page of {PageNumber} orders containing {PageSize} shoppings was received by {UserId}",
            EventId = 6010301,
            EventName = "UserShoppingsPageReceived"
        )]
        public static partial void UserShoppingsPageReceivedEventLog(
            this ILogger logger,
            int pageNumber,
            int pageSize,
            Guid userId
        );

        [LoggerMessage(
            LogLevel.Information,
            "A page of user's ({UserId}) shoppings was received using options {Options}",
            EventId = 6010401,
            EventName = "UserShoppingsPageByOptionsReceived"
        )]
        public static partial void UserShoppingsPageByOptionsReceivedEventLog(
            this ILogger logger,
            Guid userId,
            GetOrdersWithOptionsRequest? options
        );

        [LoggerMessage(
            LogLevel.Information,
            "A page of {PageNumber} orders containing {PageSize} sales was received by {UserId}",
            EventId = 6010501,
            EventName = "UserSalesPageReceived"
        )]
        public static partial void UserSalesPageReceivedEventLog(
            this ILogger logger,
            int pageNumber,
            int pageSize,
            Guid userId
        );

        [LoggerMessage(
            LogLevel.Information,
            "A page of user's ({UserId}) sales was received using options {Options}",
            EventId = 6010601,
            EventName = "UserSalesPageByOptionsReceived"
        )]
        public static partial void UserSalesPageByOptionsReceivedEventLog(
            this ILogger logger,
            Guid userId,
            GetOrdersWithOptionsRequest? options
        );

        [LoggerMessage(
            LogLevel.Information,
            "The order with Id = {OrderId} was moved by {UserId}",
            EventId = 6010701,
            EventName = "OrderStatusMoved"
        )]
        public static partial void OrderStatusMovedEventLog(
            this ILogger logger,
            string orderId,
            Guid userId
        );

        [LoggerMessage(
            LogLevel.Warning,
            "The order {OrderId} was not moved by {UserId} because the order was not found",
            EventId = 6010702,
            EventName = "OrderStatusWasNotMovedDueToResourceNotFound"
        )]
        public static partial void OrderStatusWasNotMovedDueToResourceNotFoundEventLog(
            this ILogger logger,
            string orderId,
            Guid userId
        );

        [LoggerMessage(
            LogLevel.Warning,
            "The order {OrderId} was not moved by user {UserId} because the user does not have access to order",
            EventId = 6010703,
            EventName = "OrderStatusWasNotMovedDueToAccessDenied"
        )]
        public static partial void OrderStatusWasNotMovedDueToAccessDeniedEventLog(
            this ILogger logger,
            string orderId,
            Guid userId
        );

        [LoggerMessage(
            LogLevel.Warning,
            "The order {OrderId} was not moved by user {UserId} because the order could not be moved. Exception: {ExceptionDetails}",
            EventId = 6010704,
            EventName = "OrderStatusWasNotMovedDueToIncorrectState"
        )]
        public static partial void OrderStatusWasNotMovedDueToIncorrectStateEventLog(
            this ILogger logger,
            string orderId,
            Guid userId,
            string exceptionDetails
        );

        [LoggerMessage(
            LogLevel.Information,
            "The order with Id = {OrderId} was canceled by {UserId}",
            EventId = 6010801,
            EventName = "OrderCanceled"
        )]
        public static partial void OrderCanceledEventLog(
            this ILogger logger,
            string orderId,
            Guid userId
        );

        [LoggerMessage(
            LogLevel.Warning,
            "The order {OrderId} was not canceled by {UserId} because the order was not found",
            EventId = 6010802,
            EventName = "OrderWasNotCanceledDueToResourceNotFound"
        )]
        public static partial void OrderWasNotCanceledDueToResourceNotFoundEventLog(
            this ILogger logger,
            string orderId,
            Guid userId
        );

        [LoggerMessage(
            LogLevel.Warning,
            "The order {OrderId} was not canceled by user {UserId} because the user does not have access to order",
            EventId = 6010803,
            EventName = "OrderWasNotCanceledDueToAccessDenied"
        )]
        public static partial void OrderWasNotCanceledDueToAccessDeniedEventLog(
            this ILogger logger,
            string orderId,
            Guid userId
        );

        [LoggerMessage(
            LogLevel.Warning,
            "The order {OrderId} was not canceled by user {UserId} because the order could not be canceled. Exception: {ExceptionDetails}",
            EventId = 6010804,
            EventName = "OrderWasNotCanceledDueToIncorrectState"
        )]
        public static partial void OrderWasNotCanceledDueToIncorrectStateEventLog(
            this ILogger logger,
            string orderId,
            Guid userId,
            string exceptionDetails
        );
    }
}
