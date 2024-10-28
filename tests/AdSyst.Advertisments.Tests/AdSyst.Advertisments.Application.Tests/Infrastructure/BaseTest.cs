using Moq;
using AdSyst.Common.Application.Abstractions.Authentication;
using AdSyst.Common.Application.Abstractions.Clock;

namespace AdSyst.Advertisments.Application.Tests.Infrastructure
{
    public abstract class BaseTest
    {
        protected static Guid TestUserId => Guid.Parse("9c6876c4-ecb1-4130-9abf-1f8c5752e305");
        protected static CancellationToken Token => default;
        protected Mock<IUserContext> UserContextMock { get; }
        protected Mock<IDateTimeProvider> DateTimeProviderMock { get; }

        protected static DateTimeOffset TestClock() => new(2024, 6, 13, 13, 30, 0, new(0, 1, 0));

        public BaseTest()
        {
            UserContextMock = new();
            UserContextMock.Setup(c => c.UserId).Returns(TestUserId);

            DateTimeProviderMock = new();
            DateTimeProviderMock.Setup(p => p.UtcWithOffsetNow).Returns(TestClock());
            DateTimeProviderMock.Setup(p => p.UtcNow).Returns(TestClock().DateTime);
        }
    }
}
