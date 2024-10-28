using MassTransit;
using AdSyst.Common.Contracts.EmailMessages;
using AdSyst.EmailWorker.Interfaces;
using AdSyst.EmailWorker.Models;

namespace AdSyst.EmailWorker.Consumers
{
    public class EmailMessageSentEventConsumer : IConsumer<EmailMessageSentEvent>
    {
        private readonly ILogger<EmailMessageSentEventConsumer> _logger;
        private readonly IMessageManager _messageManager;

        public EmailMessageSentEventConsumer(
            ILogger<EmailMessageSentEventConsumer> logger,
            IMessageManager messageManager
        )
        {
            _logger = logger;
            _messageManager = messageManager;
        }

        public async Task Consume(ConsumeContext<EmailMessageSentEvent> context)
        {
            var eventMessage = context.Message;
            _logger.EmailMessageSentEventConsumedEventLog();
            var message = new Message(
                eventMessage.AddressesTo,
                eventMessage.Subject,
                eventMessage.Text,
                eventMessage.IsHtml
            );
            await _messageManager.AddMessageAsync(message, context.CancellationToken);
        }
    }
}
