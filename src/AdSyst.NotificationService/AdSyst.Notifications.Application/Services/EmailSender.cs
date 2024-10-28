using MassTransit;
using AdSyst.Common.Contracts.EmailMessages;
using AdSyst.Notifications.BusinessLayer.Interfaces;
using AdSyst.Notifications.BusinessLayer.Models;

namespace AdSyst.Notifications.Application.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EmailSender(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task SendMessageAsync(
            NotifyMessage messageContent,
            string[] addressesTo,
            CancellationToken cancellationToken
        )
        {
            var eventModel = new EmailMessageSentEvent(
                addressesTo,
                messageContent.Subject,
                messageContent.Text,
                messageContent.IsHtml
            );

            return _publishEndpoint.Publish(eventModel, cancellationToken);
        }
    }
}
