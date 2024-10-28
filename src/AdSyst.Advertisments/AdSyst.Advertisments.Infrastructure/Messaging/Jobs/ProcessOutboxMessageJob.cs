using MediatR;
using AdSyst.Advertisments.Domain.Abstractions;
using AdSyst.Advertisments.Infrastructure.Messaging.Persistence;
using AdSyst.Advertisments.Infrastructure.Messaging.Settings;
using AdSyst.Common.Application.Abstractions.Data;
using Quartz;

namespace AdSyst.Advertisments.Infrastructure.Messaging.Jobs
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxMessageJob : ProcessEventMessageJob<IDomainEvent>
    {
        public ProcessOutboxMessageJob(
            EventProcessOptions options,
            ISqlConnectionFactory connectionFactory,
            OutboxMessageRepository repository,
            IPublisher publisher
        )
            : base(options, connectionFactory, repository, publisher) { }
    }
}
