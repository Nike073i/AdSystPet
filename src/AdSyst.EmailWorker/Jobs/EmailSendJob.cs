using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using AdSyst.EmailWorker.Exceptions;
using AdSyst.EmailWorker.Interfaces;
using AdSyst.EmailWorker.Models;
using AdSyst.EmailWorker.Settings;

namespace AdSyst.EmailWorker.Jobs
{
    public class EmailSendJob : BackgroundService
    {
        private readonly TimeSpan _period = new(0, 1, 0);
        private readonly EmailSettings _emailConfigs;
        private readonly ILogger<EmailSendJob> _logger;
        private readonly IMessageManager _messageManager;

        public EmailSendJob(
            ILogger<EmailSendJob> logger,
            IMessageManager messagesQueue,
            EmailSettings emailConfigsOptions
        )
        {
            _emailConfigs = emailConfigsOptions;
            _logger = logger;
            _messageManager = messagesQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.JobStartedEventLog();
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.IterationStarted();
                var messages = await _messageManager.GetMessagesAsync(
                    cancellationToken: stoppingToken
                );
                if (messages.Any())
                {
                    _logger.MessagesFoundedEventLog(messages.Count);
                    using var client = await CreateConnectionAsync();
                    foreach (var message in messages)
                    {
                        await SendMessageAsync(client, message, stoppingToken);
                    }
                    await client.DisconnectAsync(true, stoppingToken);
                }
                _logger.IterationFinished();
                await Task.Delay(_period, stoppingToken);
            }
        }

        private async Task SendMessageAsync(
            SmtpClient client,
            Message message,
            CancellationToken cancellationToken
        )
        {
            using var emailMessage = new MimeMessage();
            emailMessage
                .From
                .Add(new MailboxAddress(_emailConfigs.AddressFromName, _emailConfigs.AddressFrom));
            emailMessage
                .To
                .AddRange(
                    message.AddressesTo.Select(emailTo => new MailboxAddress(string.Empty, emailTo))
                );
            emailMessage.Subject = message.Subject;

            var textFormat = message.IsHtml
                ? MimeKit.Text.TextFormat.Html
                : MimeKit.Text.TextFormat.Plain;
            using var body = new TextPart(textFormat) { Text = message.Text };
            emailMessage.Body = body;
            await client.SendAsync(emailMessage, cancellationToken);
            _logger.MessageSentEventLog();
        }

        private async Task<SmtpClient> CreateConnectionAsync()
        {
            var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(
                    _emailConfigs.Host,
                    _emailConfigs.TlsPort,
                    SecureSocketOptions.StartTls
                );
                await client.AuthenticateAsync(_emailConfigs.Username, _emailConfigs.Password);
                _logger.ConnectionЕstablishedEventLog();
            }
            catch
            {
                string message = "С указанными данными не удалось установить подключение к почте";
                _logger.ConnectionWasNotЕstablishedEventLog(message);
                throw new EmailServiceConnectionException(message);
            }
            return client;
        }
    }
}
