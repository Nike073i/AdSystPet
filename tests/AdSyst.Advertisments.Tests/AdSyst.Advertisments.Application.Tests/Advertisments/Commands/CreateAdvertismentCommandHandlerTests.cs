using Moq;
using AdSyst.Advertisments.Application.Advertisments.Commands.CreateAdvertisment;
using AdSyst.Advertisments.Application.Tests.Advertisments.Shared;
using AdSyst.Advertisments.Application.Tests.Infrastructure;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Advertisments.Domain.AdvertismentTypes;
using AdSyst.Advertisments.Domain.Categories;

namespace AdSyst.Advertisments.Application.Tests.Advertisments.Commands
{
    public class CreateAdvertismentCommandHandlerTests : BaseCommandTest
    {
        private readonly Mock<IAdvertismentRepository> _adRepositoryMock;
        private readonly Mock<IAdvertismentTypeRepository> _typeRepositoryMock;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly CreateAdvertismentCommandHandler _handler;

        private readonly Guid _testTypeId = Guid.Parse("14dc9dff-caff-49c1-a04f-c3216bac6576");
        private readonly Guid _testCategoryId = Guid.Parse("fe150ca2-2bfd-48c4-8cd7-4a9c2ee07500");

        public CreateAdvertismentCommandHandlerTests()
        {
            _adRepositoryMock = new Mock<IAdvertismentRepository>();

            _typeRepositoryMock = new Mock<IAdvertismentTypeRepository>();
            _typeRepositoryMock
                .Setup(r => r.IsExistsAsync(_testTypeId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _categoryRepositoryMock
                .Setup(r => r.IsExistsAsync(_testCategoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _handler = new(
                _adRepositoryMock.Object,
                _categoryRepositoryMock.Object,
                _typeRepositoryMock.Object,
                UnitOfWorkMock.Object,
                UserContextMock.Object,
                DateTimeProviderMock.Object
            );
        }

        [Fact]
        public void Handle_Should_CompleteSuccess()
        {
            // Arrange
            var command = new CreateAdvertismentCommand(
                AdvertismentData.Title,
                AdvertismentData.Description,
                _testCategoryId,
                _testTypeId,
                AdvertismentData.Price,
                AdvertismentData.Images
            );

            // Act
            var result = _handler.Handle(command, Token).Await();

            // Assert
            result.IsError.ShouldBeFalse();
            UnitOfWorkMock.VerifySaving();
        }

        [Fact]
        public void Handle_WhenUserIsNotAuthenticated_Should_ReturnError()
        {
            // Arrange
            UserContextMock.Reset();
            var command = new CreateAdvertismentCommand(
                AdvertismentData.Title,
                AdvertismentData.Description,
                _testCategoryId,
                _testTypeId,
                AdvertismentData.Price,
                AdvertismentData.Images
            );

            // Act
            var result = _handler.Handle(command, Token).Await();

            // Assert
            result.IsError.ShouldBeTrue();
            result.FirstError.ShouldBe(AdvertismentErrors.Unauthorized);
        }

        [Fact]
        public void Handle_WhenTypeNotFound_Should_ReturnError()
        {
            // Arrange
            var typeId = Guid.Parse("c032b311-9e89-49ca-b863-0b5bcbb35287");
            var command = new CreateAdvertismentCommand(
                AdvertismentData.Title,
                AdvertismentData.Description,
                _testCategoryId,
                typeId,
                AdvertismentData.Price,
                AdvertismentData.Images
            );

            // Act
            var result = _handler.Handle(command, Token).Await();

            // Assert
            result.IsError.ShouldBeTrue();
            result.FirstError.ShouldBe(AdvertismentTypeErrors.NotFound);
        }

        [Fact]
        public void Handle_WhenCategoryNotFound_Should_ReturnError()
        {
            // Arrange
            var categoryId = Guid.Parse("b5e6aa5f-65ae-469c-9592-45de23fdbb9d");
            var command = new CreateAdvertismentCommand(
                AdvertismentData.Title,
                AdvertismentData.Description,
                categoryId,
                _testTypeId,
                AdvertismentData.Price,
                AdvertismentData.Images
            );

            // Act
            var result = _handler.Handle(command, Token).Await();

            // Assert
            result.IsError.ShouldBeTrue();
            result.FirstError.ShouldBe(CategoryErrors.NotFound);
        }
    }
}
