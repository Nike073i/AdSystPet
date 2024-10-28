using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdSyst.Advertisments.Infrastructure.Messaging.Models;
using AdSyst.Common.Infrastructure.Data.Configurations;

namespace AdSyst.Advertisments.Infrastructure.Messaging.Persistence
{
    public class EventMessageWriteConfiguration
        : IWriteConfiguration,
            IEntityTypeConfiguration<EventMessage>
    {
        public void Configure(EntityTypeBuilder<EventMessage> builder)
        {
            builder.UseTpcMappingStrategy();
            builder.HasIndex(m => m.OccurredOnUtc);
        }
    }
}
