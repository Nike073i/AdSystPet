namespace AdSyst.Advertisments.Infrastructure.Messaging.Settings
{
    public class EventProcessOptions
    {
        public required int BatchSize { get; set; }
        public required int IntervalInSeconds { get; set; }
    }
}
