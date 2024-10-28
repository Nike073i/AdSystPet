using AdSyst.Common.Application.Abstractions.Clock;

namespace AdSyst.Common.Infrastructure.Clock
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset UtcWithOffsetNow => DateTimeOffset.UtcNow;

        public DateTime UtcNow => DateTime.UtcNow;
    }
}
