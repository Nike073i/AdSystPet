using System.Globalization;
using AdSyst.AuthService.ApplicationLayer.Email;
using AdSyst.Common.BusinessLayer.Options;

namespace AdSyst.AuthService.Application.Users.DomainEvents.UserCreated
{
    public class EmailConfirmationMessageContentProvider : IEmailConfirmationMessageContentProvider
    {
        private readonly MessageTemplate _template;

        public EmailConfirmationMessageContentProvider(MessageTemplate template)
        {
            _template = template;
        }

        public Task<Message> GetMessageAsync(
            string confirmLink,
            CancellationToken cancellationToken
        )
        {
            string text = string.Format(
                CultureInfo.CurrentCulture,
                _template.TextTemplate,
                confirmLink
            );
            var message = new Message(_template.Subject, text);
            return Task.FromResult(message);
        }
    }
}
