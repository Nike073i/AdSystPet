using AdSyst.Advertisments.Infrastructure.Data.Contexts;
using AdSyst.Common.Application.Abstractions.Clock;

namespace AdSyst.Advertisments.Infrastructure.Messaging.Persistence
{
    public class InboxMessageRepository : EventMessageRepository
    {
        public InboxMessageRepository(IDateTimeProvider dateTimeProvider)
            : base(dateTimeProvider) { }

        protected override string TableName => nameof(AdvertismentDbContext.InboxMessages);
    }
}
