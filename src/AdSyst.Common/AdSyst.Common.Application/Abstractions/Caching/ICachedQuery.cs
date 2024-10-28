namespace AdSyst.Common.Application.Abstractions.Caching
{
    public interface ICachedQuery
    {
        string CacheKey { get; }
        TimeSpan? ExpirationTime { get; }
    }
}
