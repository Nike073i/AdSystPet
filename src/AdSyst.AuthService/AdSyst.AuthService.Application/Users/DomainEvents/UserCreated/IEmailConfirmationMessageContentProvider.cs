using AdSyst.AuthService.ApplicationLayer.Email;

namespace AdSyst.AuthService.Application.Users.DomainEvents.UserCreated
{
    public interface IEmailConfirmationMessageContentProvider
    {
        Task<Message> GetMessageAsync(
            string confirmLink,
            CancellationToken cancellationToken = default
        );
    }
}
