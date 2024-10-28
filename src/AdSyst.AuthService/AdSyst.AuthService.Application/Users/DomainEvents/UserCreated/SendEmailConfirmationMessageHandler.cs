using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using AdSyst.AuthService.Application.Routing;
using AdSyst.AuthService.Domain;
using AdSyst.AuthService.Domain.Events;
using AdSyst.Common.Contracts.EmailMessages;

namespace AdSyst.AuthService.Application.Users.DomainEvents.UserCreated
{
    public class SendEmailConfirmationMessageHandler : INotificationHandler<UserCreatedDomainEvent>
    {
        private readonly ILinkService _linkService;
        private readonly IUserManager _userManager;
        private readonly IEmailConfirmationMessageContentProvider _messageContentProvider;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<SendEmailConfirmationMessageHandler> _logger;

        public SendEmailConfirmationMessageHandler(
            IUserManager userManager,
            IEmailConfirmationMessageContentProvider messageContentProvider,
            IPublishEndpoint publishEndpoint,
            ILinkService linkService,
            ILogger<SendEmailConfirmationMessageHandler> logger
        )
        {
            _userManager = userManager;
            _messageContentProvider = messageContentProvider;
            _publishEndpoint = publishEndpoint;
            _linkService = linkService;
            _logger = logger;
        }

        public async Task Handle(
            UserCreatedDomainEvent notification,
            CancellationToken cancellationToken
        )
        {
            string userId = notification.UserId;
            var getResult = await _userManager.GetByIdAsync(userId, cancellationToken);
            if (getResult.IsError)
            {
                _logger.UserNotFound(userId);
                return;
            }

            var user = getResult.Value;

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(
                user,
                cancellationToken
            );
            string confirmLink = _linkService.CreateConfirmLink(token, userId);
            var message = await _messageContentProvider.GetMessageAsync(
                confirmLink,
                cancellationToken
            );
            var eventModel = new EmailMessageSentEvent(
                notification.Email,
                message.Subject,
                message.Text,
                false
            );
            await _publishEndpoint.Publish(eventModel, cancellationToken);
        }
    }
}
