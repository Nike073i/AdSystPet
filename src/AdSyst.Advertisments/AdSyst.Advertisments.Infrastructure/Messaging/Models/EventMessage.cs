namespace AdSyst.Advertisments.Infrastructure.Messaging.Models
{
    public abstract class EventMessage
    {
        public Guid Id { get; set; }
        public required string Content { get; set; }
        public required string Type { get; set; }
        public DateTimeOffset OccurredOnUtc { get; set; }
        public DateTimeOffset? ProcessedOnUtc { get; set; }
        public string? Error { get; set; }
    }
}
