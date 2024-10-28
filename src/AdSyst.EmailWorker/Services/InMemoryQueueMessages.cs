using System.Collections.Concurrent;
using AdSyst.EmailWorker.Interfaces;
using AdSyst.EmailWorker.Models;

namespace AdSyst.EmailWorker.Services
{
    public class InMemoryQueueMessages : IMessageManager
    {
        private readonly ConcurrentQueue<Message> _messages;

        public InMemoryQueueMessages()
        {
            _messages = new();
        }

        public Task AddMessageAsync(Message message, CancellationToken cancellationToken)
        {
            _messages.Enqueue(message);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyCollection<Message>> GetMessagesAsync(
            int maxCount,
            CancellationToken cancellationToken
        )
        {
            var list = new List<Message>();

            for (int i = 0; i < maxCount && _messages.Any(); i++)
            {
                _messages.TryDequeue(out var message);
                list.Add(message!);
            }

            return Task.FromResult<IReadOnlyCollection<Message>>(list);
        }
    }
}
