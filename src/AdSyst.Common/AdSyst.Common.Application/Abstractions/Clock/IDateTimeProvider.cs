namespace AdSyst.Common.Application.Abstractions.Clock
{
    public interface IDateTimeProvider
    {
        DateTimeOffset UtcWithOffsetNow { get; }
        DateTime UtcNow { get; }
    }
}
