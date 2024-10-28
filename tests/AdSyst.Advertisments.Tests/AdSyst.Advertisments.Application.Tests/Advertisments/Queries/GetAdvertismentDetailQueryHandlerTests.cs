using Moq;
using AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentDetail;
using AdSyst.Advertisments.Application.Tests.Advertisments.Shared;
using AdSyst.Advertisments.Application.Tests.Infrastructure;
using AdSyst.Advertisments.Domain.Advertisments;

namespace AdSyst.Advertisments.Application.Tests.Advertisments.Queries
{
    public class GetAdvertismentDetailQueryHandlerTests : BaseQueryTest
    {
        private readonly GetAdvertismentDetailQueryHandler _handler;
        private readonly Guid _testAdId = Guid.Parse("90abb5aa-9d90-4c3e-b5a2-1a14a4c6b883");
        private readonly Guid _authorId = Guid.Parse("6ae0f7b5-5e11-4046-99cc-507fa60d075a");

        public GetAdvertismentDetailQueryHandlerTests()
        {
            var query = new GetAdvertismentDetailQuery(_testAdId);
            string cacheKey = query.CacheKey;

            var model = CreateModel(_testAdId, AdvertismentStatus.Active, _authorId);
            CacheServiceMock
                .Setup(
                    s =>
                        s.GetAsync(
                            It.IsAny<string>(),
                            It.IsAny<Func<CancellationToken, Task<AdvertismentDetailViewModel?>>>(),
                            It.IsAny<TimeSpan?>(),
                            It.IsAny<CancellationToken>()
                        )
                )
                .ReturnsAsync(model);

            UserContextMock.Setup(c => c.UserId).Returns(_authorId);

            var adServiceMock = new Mock<IGetAdvertismentDetailService>();

            _handler = new(adServiceMock.Object, UserContextMock.Object, CacheServiceMock.Object);
        }

        [Fact]
        public void Handle_Should_ReturnData()
        {
            // Arrange
            var query = new GetAdvertismentDetailQuery(_testAdId);

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
            CacheServiceMock.Reset();
            var query = new GetAdvertismentDetailQuery(_testAdId);

            // Act
            var result = _handler.Handle(query, Token).Await();

            // Assert
            result.IsError.ShouldBeTrue();
            result.FirstError.ShouldBe(AdvertismentErrors.NotFound);
        }

        [Fact]
        public void Handle_WhenAdvertismentNotActiveAndQueryFromAuthor_Should_ReturnData()
        {
            // Arrange
            var query = new GetAdvertismentDetailQuery(_testAdId);
            var model = CreateModel(_testAdId, AdvertismentStatus.Archival, _authorId);
            CacheServiceMock
                .Setup(
                    s =>
                        s.GetAsync(
                            query.CacheKey,
                            It.IsAny<Func<CancellationToken, Task<AdvertismentDetailViewModel?>>>(),
                            It.IsAny<TimeSpan?>(),
                            It.IsAny<CancellationToken>()
                        )
                )
                .ReturnsAsync(model);

            // Act
            var result = _handler.Handle(query, Token).Await();

            // Assert
            result.IsError.ShouldBeFalse();
            result.Value.ShouldNotBeNull();
        }

        [Fact]
        public void Handle_WhenAdvertismentNotActiveAndQueryFromSomeoneElse_Should_ReturnErrror()
        {
            // Arrange
            var query = new GetAdvertismentDetailQuery(_testAdId);
            var model = CreateModel(_testAdId, AdvertismentStatus.Archival, _authorId);
            CacheServiceMock
                .Setup(
                    s =>
                        s.GetAsync(
                            query.CacheKey,
                            It.IsAny<Func<CancellationToken, Task<AdvertismentDetailViewModel?>>>(),
                            It.IsAny<TimeSpan?>(),
                            It.IsAny<CancellationToken>()
                        )
                )
                .ReturnsAsync(model);
            UserContextMock.Reset();

            // Act
            var result = _handler.Handle(query, Token).Await();

            // Assert
            result.IsError.ShouldBeTrue();
            result.FirstError.ShouldBe(AdvertismentErrors.NotFound);
        }

        private static AdvertismentDetailViewModel CreateModel(
            Guid id,
            AdvertismentStatus status,
            Guid authorId
        ) =>
            new(
                id,
                AdvertismentData.Title,
                AdvertismentData.Description,
                AdvertismentData.Type.Id,
                AdvertismentData.Type.Name,
                AdvertismentData.Category.Id,
                AdvertismentData.Category.Name,
                AdvertismentData.Price,
                TestClock(),
                status,
                authorId,
                AdvertismentData.Images
            );
    }
}
