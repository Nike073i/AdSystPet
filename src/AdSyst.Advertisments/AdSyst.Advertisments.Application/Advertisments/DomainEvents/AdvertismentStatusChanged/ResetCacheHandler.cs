using MediatR;
using AdSyst.Advertisments.Domain.Advertisments.Events;
using AdSyst.Common.Application.Abstractions.Caching;

namespace AdSyst.Advertisments.Application.Advertisments.DomainEvents.AdvertismentStatusChanged
{
    public class ResetCacheHandler : INotificationHandler<AdvertismentStatusChangedDomainEvent>
    {
        private readonly ICacheService _cacheService;

        public ResetCacheHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public Task Handle(
            AdvertismentStatusChangedDomainEvent notification,
            CancellationToken cancellationToken
        ) =>
            _cacheService.RemoveAsync(
                $"Advertisments-{notification.AdvertismentId}",
                cancellationToken
            );
    }
}
