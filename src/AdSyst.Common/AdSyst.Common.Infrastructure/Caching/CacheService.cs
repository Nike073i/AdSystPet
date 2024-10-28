using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using AdSyst.Common.Application.Abstractions.Caching;

namespace AdSyst.Common.Infrastructure.Caching
{
    internal class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T?> GetAsync<T>(string cacheKey, CancellationToken cancellationToken)
        {
            byte[]? cachedValue = await _distributedCache.GetAsync(cacheKey, cancellationToken);
            return cachedValue == null ? default : Deserialize<T>(cachedValue);
        }

        public async Task<T?> GetAsync<T>(
            string cacheKey,
            Func<CancellationToken, Task<T?>> factory,
            TimeSpan? expiration = null,
            CancellationToken cancellationToken = default
        )
        {
            var cachedValue = await GetAsync<T>(cacheKey, cancellationToken);
            if (cachedValue != null)
                return cachedValue;
            var value = await factory(cancellationToken);
            if (value == null)
                return default;
            await SetAsync(cacheKey, value, expiration, cancellationToken);
            return value;
        }

        public Task RemoveAsync(string cacheKey, CancellationToken cancellationToken) =>
            _distributedCache.RemoveAsync(cacheKey, cancellationToken);

        public Task SetAsync<T>(
            string cacheKey,
            T value,
            TimeSpan? expiration,
            CancellationToken cancellationToken
        )
        {
            byte[] bytes = Serialize(value);
            return _distributedCache.SetAsync(
                cacheKey,
                bytes,
                CacheOptions.Create(expiration),
                cancellationToken
            );
        }

        private static T Deserialize<T>(ReadOnlySpan<byte> value) =>
            JsonSerializer.Deserialize<T>(value)!;

        private static byte[] Serialize<T>(T value) => JsonSerializer.SerializeToUtf8Bytes(value);
    }
}
