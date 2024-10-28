namespace AdSyst.Advertisments.Domain.Abstractions
{
    public abstract record DomainEvent : IDomainEvent
    {
        protected DomainEvent()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; init; }
    }
}
