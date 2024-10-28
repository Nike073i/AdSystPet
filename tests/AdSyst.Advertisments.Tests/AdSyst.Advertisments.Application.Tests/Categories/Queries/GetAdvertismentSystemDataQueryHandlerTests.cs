using Moq;
using AdSyst.Advertisments.Application.Categories.Queries.GetCategoryById;
using AdSyst.Advertisments.Application.Tests.Advertisments.Shared;
using AdSyst.Advertisments.Application.Tests.Infrastructure;
using AdSyst.Advertisments.Domain.Categories;

namespace AdSyst.Advertisments.Application.Tests.Categories.Queries
{
    public class GetCategoryByIdQueryHandlerTests : BaseQueryTest
    {
        private readonly Mock<IGetCategoryService> _serviceMock;
        private readonly GetCategoryByIdQueryHandler _handler;
        private readonly Guid _testId = Guid.Parse("cd045885-f766-492c-9c18-a873595cfb4a");

        public GetCategoryByIdQueryHandlerTests()
        {
            var model = new CategoryViewModel(
                _testId,
                AdvertismentData.Category.Name,
                Enumerable.Empty<CategoryViewModel>()
            );
            _serviceMock = new();
            _serviceMock.Setup(s => s.GetAsync(_testId, Token)).ReturnsAsync(model);

            _handler = new(_serviceMock.Object);
        }

        [Fact]
        public void Handle_Should_ReturnData()
        {
            // Arrange
            var query = new GetCategoryByIdQuery(_testId);

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
            _serviceMock.Reset();
            var query = new GetCategoryByIdQuery(_testId);

            // Act
            var result = _handler.Handle(query, Token).Await();

            // Assert
            result.IsError.ShouldBeTrue();
            result.FirstError.ShouldBe(CategoryErrors.NotFound);
        }
    }
}
