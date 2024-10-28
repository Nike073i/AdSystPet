using Moq;
using AdSyst.Common.Application.Abstractions.Caching;

namespace AdSyst.Advertisments.Application.Tests.Infrastructure
{
    public abstract class BaseQueryTest : BaseTest
    {
        protected Mock<ICacheService> CacheServiceMock { get; }

        protected BaseQueryTest()
        {
            CacheServiceMock = new();
        }
    }
}
