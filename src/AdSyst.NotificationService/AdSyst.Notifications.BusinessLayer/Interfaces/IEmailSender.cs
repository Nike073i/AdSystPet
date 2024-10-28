using AdSyst.Notifications.BusinessLayer.Models;

namespace AdSyst.Notifications.BusinessLayer.Interfaces
{
    public interface IEmailSender
    {
        Task SendMessageAsync(
            NotifyMessage messageContent,
            string[] addressesTo,
            CancellationToken cancellationToken = default
        );
    }
}
