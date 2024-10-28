using AdSyst.Notifications.Application.Enums;
using AdSyst.Notifications.Application.Exceptions;

namespace AdSyst.Notifications.Application.Services.Templates
{
    public class TemplateProvider : ITemplateProvider
    {
        private readonly EmailTemplateOptions _options;

        public TemplateProvider(EmailTemplateOptions options)
        {
            _options = options;
        }

        public MessageTemplate GetTemplate(NotificationEvent notificationEvent)
        {
            bool isExists = _options
                .Templates
                .TryGetValue(notificationEvent, out var templateConfig);
            if (!isExists)
                throw new MessageTemplateNotFoundException(notificationEvent);

            var template = templateConfig! with
            {
                TemplatePath = GetAbsoluteFilePath(templateConfig.TemplatePath)
            };

            return template;
        }

        private string GetAbsoluteFilePath(string relativePath) =>
            Path.GetFullPath(Path.Combine(_options.ContentDirectory, relativePath));
    }
}
