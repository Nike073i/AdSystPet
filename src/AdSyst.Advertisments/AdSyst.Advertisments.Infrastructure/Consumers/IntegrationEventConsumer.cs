using MassTransit;
using Newtonsoft.Json;
using AdSyst.Advertisments.Infrastructure.Data.Contexts;
using AdSyst.Advertisments.Infrastructure.Messaging.Models;
using AdSyst.Common.Application.Abstractions.Clock;
using AdSyst.Common.Contracts.Abstractions;

namespace AdSyst.Advertisments.Infrastructure.Consumers
{
    internal class IntegrationEventConsumer<TIntegrationEvent> : IConsumer<TIntegrationEvent>
        where TIntegrationEvent : class, IIntegrationEvent
    {
        private readonly AdvertismentDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;

        public IntegrationEventConsumer(
            AdvertismentDbContext dbContext,
            IDateTimeProvider dateTimeProvider
        )
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task Consume(ConsumeContext<TIntegrationEvent> context)
        {
            var message = CreateMessage(context.Message);
            _dbContext.InboxMessages.Add(message);
            await _dbContext.SaveChangesAsync(context.CancellationToken);
        }

        private InboxMessage CreateMessage(TIntegrationEvent integrationEvent)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            var message = new InboxMessage
            {
                OccurredOnUtc = _dateTimeProvider.UtcWithOffsetNow,
                Type = integrationEvent.GetType().Name,
                Content = JsonConvert.SerializeObject(integrationEvent, serializerSettings)
            };
            return message;
        }
    }
}
