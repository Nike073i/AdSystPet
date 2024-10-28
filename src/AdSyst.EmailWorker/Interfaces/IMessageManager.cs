using AdSyst.EmailWorker.Models;

namespace AdSyst.EmailWorker.Interfaces
{
    public interface IMessageManager
    {
        Task AddMessageAsync(Message message, CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<Message>> GetMessagesAsync(
            int maxCount = 25,
            CancellationToken cancellationToken = default
        );
    }
}
