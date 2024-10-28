using AdSyst.Notifications.BusinessLayer.Models;

namespace AdSyst.Notifications.BusinessLayer.Interfaces
{
    public interface INotifyManager
    {
        Task NotifyUserAsync(
            Guid userId,
            NotifyMessage notifyMessage,
            CancellationToken cancellationToken = default
        );
    }
}
