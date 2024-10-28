using System.Data;
using AdSyst.Advertisments.Infrastructure.Messaging.Models;

namespace AdSyst.Advertisments.Infrastructure.Messaging.Persistence
{
    public interface IEventMessageRepository
    {
        Task<IReadOnlyList<EventMessageResponse>> GetWithLock(
            int batchSize,
            IDbTransaction transaction,
            IDbConnection connection
        );

        Task UpdateMessageStatus(
            Guid id,
            string? error,
            IDbTransaction transaction,
            IDbConnection connection
        );
    }
}
