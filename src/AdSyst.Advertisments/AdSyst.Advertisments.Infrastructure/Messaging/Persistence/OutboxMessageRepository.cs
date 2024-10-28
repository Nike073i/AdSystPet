using AdSyst.Advertisments.Infrastructure.Data.Contexts;
using AdSyst.Common.Application.Abstractions.Clock;

namespace AdSyst.Advertisments.Infrastructure.Messaging.Persistence
{
    public class OutboxMessageRepository : EventMessageRepository
    {
        public OutboxMessageRepository(IDateTimeProvider dateTimeProvider)
            : base(dateTimeProvider) { }

        protected override string TableName => nameof(AdvertismentDbContext.OutboxMessages);
    }
}
