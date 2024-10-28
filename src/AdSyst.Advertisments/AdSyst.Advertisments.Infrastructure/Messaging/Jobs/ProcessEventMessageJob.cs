using MediatR;
using Newtonsoft.Json;
using AdSyst.Advertisments.Infrastructure.Messaging.Persistence;
using AdSyst.Advertisments.Infrastructure.Messaging.Settings;
using AdSyst.Common.Application.Abstractions.Data;
using Quartz;

namespace AdSyst.Advertisments.Infrastructure.Messaging.Jobs
{
    public abstract class ProcessEventMessageJob<T> : IJob
    {
        private readonly JsonSerializerSettings _serializerSettings =
            new() { TypeNameHandling = TypeNameHandling.All };
        private readonly EventProcessOptions _options;
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IEventMessageRepository _repository;
        private readonly IPublisher _publisher;

        public ProcessEventMessageJob(
            EventProcessOptions options,
            ISqlConnectionFactory connectionFactory,
            IEventMessageRepository repository,
            IPublisher publisher
        )
        {
            _options = options;
            _connectionFactory = connectionFactory;
            _repository = repository;
            _publisher = publisher;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using var connection = _connectionFactory.OpenConnection();
            using var transaction = connection.BeginTransaction();

            var messages = await _repository.GetWithLock(
                _options.BatchSize,
                transaction,
                connection
            );

            foreach (var message in messages)
            {
                Exception? exception = null;
                try
                {
                    var @event = JsonConvert.DeserializeObject<T>(
                        message.Content,
                        _serializerSettings
                    )!;

                    await _publisher.Publish(@event, context.CancellationToken);
                }
                catch (Exception caughtException)
                {
                    exception = caughtException;
                }

                await _repository.UpdateMessageStatus(
                    message.Id,
                    exception?.Message,
                    transaction,
                    connection
                );
            }

            transaction.Commit();
        }
    }
}
