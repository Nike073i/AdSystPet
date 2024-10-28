using MediatR;
using AdSyst.Advertisments.Domain.Advertisments.Events;
using AdSyst.Common.Application.Abstractions.Caching;

namespace AdSyst.Advertisments.Application.Advertisments.DomainEvents.AdvertismentUpdated
{
    public class ResetCacheHandler : INotificationHandler<AdvertismentUpdatedDomainEvent>
    {
        private readonly ICacheService _cacheService;

        public ResetCacheHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public Task Handle(
            AdvertismentUpdatedDomainEvent notification,
            CancellationToken cancellationToken
        ) => _cacheService.RemoveAsync($"Advertisments-{notification.Id}", cancellationToken);
    }
}
