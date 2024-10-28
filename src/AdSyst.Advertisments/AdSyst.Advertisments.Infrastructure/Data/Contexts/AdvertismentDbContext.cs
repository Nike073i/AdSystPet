using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using AdSyst.Advertisments.Domain.Abstractions;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Advertisments.Domain.AdvertismentTypes;
using AdSyst.Advertisments.Domain.Categories;
using AdSyst.Advertisments.Infrastructure.Messaging.Models;
using AdSyst.Common.Application.Abstractions.Clock;
using AdSyst.Common.Application.Abstractions.Data;
using AdSyst.Common.Infrastructure.Data.Configurations;

namespace AdSyst.Advertisments.Infrastructure.Data.Contexts
{
    /// <summary>
    /// Контекст базы данных объявлений
    /// </summary>
    internal class AdvertismentDbContext : DbContext, IUnitOfWork
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public AdvertismentDbContext(
            DbContextOptions<AdvertismentDbContext> dbContextOptions,
            IDateTimeProvider dateTimeProvider
        )
            : base(dbContextOptions)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        /// <summary>
        /// Типы объявлений
        /// </summary>
        public virtual DbSet<AdvertismentType> AdvertismentTypes => Set<AdvertismentType>();

        /// <summary>
        /// Категории
        /// </summary>
        public virtual DbSet<Category> Categories => Set<Category>();

        /// <summary>
        /// Объявления
        /// </summary>
        public virtual DbSet<Advertisment> Advertisments => Set<Advertisment>();

        /// <summary>
        /// Изображения объявлений
        /// </summary>
        public virtual DbSet<AdvertismentImage> AdvertismentImages => Set<AdvertismentImage>();

        public virtual DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
        public virtual DbSet<InboxMessage> InboxMessages => Set<InboxMessage>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly(),
                type => type.IsAssignableTo(typeof(IWriteConfiguration))
            );

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            AddOutboxDomainEvents();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddOutboxDomainEvents()
        {
            var events = ChangeTracker
                .Entries<IHasEvents>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    var events = entity.Events;
                    entity.ClearEvents();
                    return events;
                })
                .ToList();
            var messages = CreateMessages(events);
            AddRange(messages);
        }

        private IEnumerable<OutboxMessage> CreateMessages(IEnumerable<IDomainEvent> events)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            var messages = events
                .Select(
                    e =>
                        new OutboxMessage
                        {
                            OccurredOnUtc = _dateTimeProvider.UtcWithOffsetNow,
                            Type = e.GetType().Name,
                            Content = JsonConvert.SerializeObject(e, serializerSettings)
                        }
                )
                .ToList();
            return messages;
        }
    }
}
