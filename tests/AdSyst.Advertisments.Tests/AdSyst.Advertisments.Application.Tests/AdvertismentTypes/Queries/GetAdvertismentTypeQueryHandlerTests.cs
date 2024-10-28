using Moq;
using AdSyst.Advertisments.Application.AdvertismentTypes.Queries.GetAdvertismentTypeById;
using AdSyst.Advertisments.Application.Tests.Advertisments.Shared;
using AdSyst.Advertisments.Application.Tests.Infrastructure;
using AdSyst.Advertisments.Domain.AdvertismentTypes;

namespace AdSyst.Advertisments.Application.Tests.AdvertismentTypes.Queries
{
    public class GetAdvertismentTypeQueryHandlerTests : BaseQueryTest
    {
        private readonly Mock<IGetAdvertismentTypeService> _typeServiceMock;
        private readonly GetAdvertismentTypeByIdQueryHandler _handler;
        private readonly Guid _testTypeId = Guid.Parse("e0b4e027-f355-4387-b1ff-7ea5def1ee40");

        public GetAdvertismentTypeQueryHandlerTests()
        {
            var model = new AdvertismentTypeViewModel(_testTypeId, AdvertismentData.Type.Name);
            _typeServiceMock = new();
            _typeServiceMock.Setup(s => s.GetAsync(_testTypeId, Token)).ReturnsAsync(model);

            _handler = new(_typeServiceMock.Object);
        }

        [Fact]
        public void Handle_Should_ReturnData()
        {
            // Arrange
            var query = new GetAdvertismentTypeByIdQuery(_testTypeId);

            // Act
            var result = _handler.Handle(query, Token).Await();

            // Assert
            result.IsError.ShouldBeFalse();
            result.Value.ShouldNotBeNull();
        }

        [Fact]
        public void Handle_WhenTypeNotFound_Should_ReturnErrror()
        {
            // Arrange
            _typeServiceMock.Reset();
            var query = new GetAdvertismentTypeByIdQuery(_testTypeId);

            // Act
            var result = _handler.Handle(query, Token).Await();

            // Assert
            result.IsError.ShouldBeTrue();
            result.FirstError.ShouldBe(AdvertismentTypeErrors.NotFound);
        }
    }
}
