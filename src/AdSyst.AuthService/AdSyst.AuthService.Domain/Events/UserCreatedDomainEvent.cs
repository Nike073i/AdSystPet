using MediatR;

namespace AdSyst.AuthService.Domain.Events
{
    public record UserCreatedDomainEvent : INotification
    {
        public Guid Id { get; init; }
        public string UserId { get; init; }
        public string Email { get; init; }

        public UserCreatedDomainEvent(string userId, string email, Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            UserId = userId;
            Email = email;
        }
    }
}
