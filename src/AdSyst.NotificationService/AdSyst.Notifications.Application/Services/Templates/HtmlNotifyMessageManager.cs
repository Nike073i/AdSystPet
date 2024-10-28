using AdSyst.Notifications.Application.Enums;
using AdSyst.Notifications.Application.Services.ViewEngine;
using AdSyst.Notifications.BusinessLayer.Models;

namespace AdSyst.Notifications.Application.Services.Templates
{
    public class HtmlNotifyMessageManager : INotifyMessageManager
    {
        private readonly IViewEngine _viewEngine;
        private readonly ITemplateProvider _templateProvider;

        public HtmlNotifyMessageManager(IViewEngine viewEngine, ITemplateProvider templateProvider)
        {
            _viewEngine = viewEngine;
            _templateProvider = templateProvider;
        }

        public async Task<NotifyMessage> CreateMessageAsync(
            NotificationEvent notificationEvent,
            object data,
            CancellationToken cancellationToken
        )
        {
            var template = _templateProvider.GetTemplate(notificationEvent);

            string content = await _viewEngine.CompileHtmlAsync(
                template.TemplatePath,
                data,
                cancellationToken
            );
            var message = new NotifyMessage(template.Subject, content);
            return message;
        }
    }
}
