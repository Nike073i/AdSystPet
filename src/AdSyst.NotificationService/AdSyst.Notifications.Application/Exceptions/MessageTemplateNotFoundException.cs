using AdSyst.Notifications.Application.Enums;

namespace AdSyst.Notifications.Application.Exceptions
{
    public class MessageTemplateNotFoundException : Exception
    {
        public MessageTemplateNotFoundException(NotificationEvent notificationEvent)
            : base(
                $"Не зарегистрирован шаблон сообщения для уведомления по событию {notificationEvent}"
            ) { }
    }
}
