namespace AdSyst.Advertisments.Domain.Abstractions
{
    /// <summary>
    /// Интерфейс сущности
    /// </summary>
    /// <typeparam name="TId">Тип идентификатора</typeparam>
    public abstract class Entity<TId> : IHasEvents
    {
        private readonly List<IDomainEvent> _events = new();

        protected Entity() { }

        protected Entity(TId id)
        {
            Id = id;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public TId Id { get; init; }

        public IReadOnlyList<IDomainEvent> Events => _events.ToList();

        public void ClearEvents() => _events.Clear();

        protected void Raise(IDomainEvent @event) => _events.Add(@event);
    }
}
