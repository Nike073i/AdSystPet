using Microsoft.Extensions.Caching.Distributed;

namespace AdSyst.Common.Infrastructure.Caching
{
    internal class CacheOptions
    {
        private static readonly DistributedCacheEntryOptions _defaultOptions =
            new() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5), };

        public static DistributedCacheEntryOptions Create(TimeSpan? expiration) =>
            expiration == null
                ? _defaultOptions
                : new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration };
    }
}
