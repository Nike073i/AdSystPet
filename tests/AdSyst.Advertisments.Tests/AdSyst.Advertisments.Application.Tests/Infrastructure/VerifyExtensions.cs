using Moq;
using AdSyst.Common.Application.Abstractions.Data;

namespace AdSyst.Advertisments.Application.Tests.Infrastructure
{
    public static class VerifyExtensions
    {
        public static void VerifySaving(this Mock<IUnitOfWork> mock) =>
            mock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
