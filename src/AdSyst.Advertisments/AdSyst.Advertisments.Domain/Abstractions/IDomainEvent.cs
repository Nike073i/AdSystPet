using MediatR;

namespace AdSyst.Advertisments.Domain.Abstractions
{
    public interface IDomainEvent : INotification
    {
        Guid Id { get; }
    }
}
