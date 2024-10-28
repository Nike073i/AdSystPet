using Moq;
using AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentSystemData;
using AdSyst.Advertisments.Application.Tests.Advertisments.Shared;
using AdSyst.Advertisments.Application.Tests.Infrastructure;
using AdSyst.Advertisments.Domain.Advertisments;

namespace AdSyst.Advertisments.Application.Tests.Advertisments.Queries
{
    public class GetAdvertismentSystemDataQueryHandlerTests : BaseQueryTest
    {
        private readonly Mock<IGetAdvertismentSystemDataService> _adServiceMock;
        private readonly GetAdvertismentSystemDataQueryHandler _handler;
        private readonly Guid _testAdId = Guid.Parse("260f1410-a123-4399-b40d-00a7e2805a70");

        public GetAdvertismentSystemDataQueryHandlerTests()
        {
            var model = new AdvertismentSystemViewModel(
                _testAdId,
                AdvertismentData.AuthorId,
                TestClock(),
                AdvertismentStatus.Active
            );
            _adServiceMock = new();
            _adServiceMock.Setup(s => s.GetAsync(_testAdId, Token)).ReturnsAsync(model);

            _handler = new(_adServiceMock.Object);
        }

        [Fact]
        public void Handle_Should_ReturnData()
        {
            // Arrange
            var query = new GetAdvertismentSystemDataQuery(_testAdId);

            // Act
            var result = _handler.Handle(query, Token).Await();

            // Assert
            result.IsError.ShouldBeFalse();
            result.Value.ShouldNotBeNull();
        }

        [Fact]
        public void Handle_WhenAdvertismentNotFound_Should_ReturnErrror()
        {
            // Arrange
            _adServiceMock.Reset();
            var query = new GetAdvertismentSystemDataQuery(_testAdId);

            // Act
            var result = _handler.Handle(query, Token).Await();

            // Assert
            result.IsError.ShouldBeTrue();
            result.FirstError.ShouldBe(AdvertismentErrors.NotFound);
        }
    }
}
