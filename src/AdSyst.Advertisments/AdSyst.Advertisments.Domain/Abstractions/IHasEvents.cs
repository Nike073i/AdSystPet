namespace AdSyst.Advertisments.Domain.Abstractions
{
    public interface IHasEvents
    {
        IReadOnlyList<IDomainEvent> Events { get; }

        void ClearEvents();
    }
}
