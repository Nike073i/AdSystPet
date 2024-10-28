namespace AdSyst.WebFiles.Api.Logging
{
    public static partial class ImageEventLogs
    {
        [LoggerMessage(
            LogLevel.Information,
            "The image was uploaded by user {UserId} and has resource ID = {ResourceId}",
            EventId = 6010101,
            EventName = "ImageUploaded"
        )]
        public static partial void ImageUploadedEventLog(
            this ILogger logger,
            Guid userId,
            Guid resourceId
        );

        [LoggerMessage(
            LogLevel.Warning,
            "The image ({FileName}) from user {UserId} was not loaded because it has invalid data. Exception: {ExceptionDetails}",
            EventId = 6010102,
            EventName = "ImageWasNotUploadedDueInvalidFileData"
        )]
        public static partial void ImageWasNotUploadedDueInvalidFileDataEventLog(
            this ILogger logger,
            string fileName,
            Guid userId,
            string exceptionDetails
        );
    }
}
