using Moq;
using AdSyst.Common.Application.Abstractions.Data;

namespace AdSyst.Advertisments.Application.Tests.Infrastructure
{
    public abstract class BaseCommandTest : BaseTest
    {
        protected Mock<IUnitOfWork> UnitOfWorkMock { get; } = new();
    }
}
