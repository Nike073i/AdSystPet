using AdSyst.Notifications.Application.Enums;

namespace AdSyst.Notifications.Application.Services.Templates
{
    public interface ITemplateProvider
    {
        MessageTemplate GetTemplate(NotificationEvent notificationEvent);
    }
}
