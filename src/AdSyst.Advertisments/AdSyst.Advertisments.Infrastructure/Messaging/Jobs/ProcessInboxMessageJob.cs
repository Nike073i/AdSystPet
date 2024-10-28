using MediatR;
using AdSyst.Advertisments.Infrastructure.Messaging.Persistence;
using AdSyst.Advertisments.Infrastructure.Messaging.Settings;
using AdSyst.Common.Application.Abstractions.Data;
using AdSyst.Common.Contracts.Abstractions;
using Quartz;

namespace AdSyst.Advertisments.Infrastructure.Messaging.Jobs
{
    [DisallowConcurrentExecution]
    public class ProcessInboxMessageJob : ProcessEventMessageJob<IIntegrationEvent>
    {
        public ProcessInboxMessageJob(
            EventProcessOptions options,
            ISqlConnectionFactory connectionFactory,
            InboxMessageRepository repository,
            IPublisher publisher
        )
            : base(options, connectionFactory, repository, publisher) { }
    }
}
