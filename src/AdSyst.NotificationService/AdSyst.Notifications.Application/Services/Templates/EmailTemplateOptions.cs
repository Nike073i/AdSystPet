using AdSyst.Notifications.Application.Enums;

namespace AdSyst.Notifications.Application.Services.Templates
{
    public class EmailTemplateOptions
    {
        public required Dictionary<NotificationEvent, MessageTemplate> Templates { get; set; }
        public required string ContentDirectory { get; set; }
    }
}
