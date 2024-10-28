using Moq;
using AdSyst.Advertisments.Application.Advertisments.Commands.UpdateAdvertisment;
using AdSyst.Advertisments.Application.Tests.Advertisments.Shared;
using AdSyst.Advertisments.Application.Tests.Infrastructure;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Advertisments.Domain.AdvertismentTypes;
using AdSyst.Advertisments.Domain.Categories;

namespace AdSyst.Advertisments.Application.Tests.Advertisments.Commands
{
    public class UpdateAdvertismentCommandHandlerTests : BaseCommandTest
    {
        private readonly Mock<IAdvertismentRepository> _adRepositoryMock;
        private readonly Mock<IAdvertismentTypeRepository> _typeRepositoryMock;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly UpdateAdvertismentCommandHandler _handler;

        private readonly Guid _testAdId = Guid.Parse("93b98c04-0bf1-4a04-8701-5dc62e04f91d");
        private readonly Guid _testTypeId = Guid.Parse("7a7ac639-9bde-4541-8980-52043ff6849c");
        private readonly Guid _testCategoryId = Guid.Parse("bb69b3e3-252b-49f6-a369-b8d8b820bccb");

        public UpdateAdvertismentCommandHandlerTests()
        {
            _adRepositoryMock = new Mock<IAdvertismentRepository>();
            var ad = AdvertismentData.CreateAdvertismentWithTestData(
                TestClock,
                authorId: TestUserId
            );
            _adRepositoryMock
                .Setup(r => r.GetByIdAsync(_testAdId, true, It.IsAny<CancellationToken>()))
                .ReturnsAsync(ad);

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
                _typeRepositoryMock.Object,
                _categoryRepositoryMock.Object,
                UnitOfWorkMock.Object,
                UserContextMock.Object
            );
        }

        [Fact]
        public void Handle_Should_ChangeState()
        {
            // Arrange
            var adId = _testAdId;
            string newTitle = "Новое имя объявления";
            string newDescription = "Новое описание объявления";
            decimal newPrice = 10000m;
            Guid[] newImages =
            {
                Guid.Parse("00e57813-b292-4781-b614-c6730be85187"),
                Guid.Parse("74a0fc7e-43c9-46f5-8b1f-69354435b519"),
                Guid.Parse("78020e4e-b577-47f6-a84d-d1b9e6b82269")
            };

            var command = new UpdateAdvertismentCommand(
                adId,
                newTitle,
                newDescription,
                newPrice,
                _testCategoryId,
                _testTypeId,
                newImages
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
            var adId = _testAdId;
            UserContextMock.Reset();
            string newTitle = "Новое имя объявления";
            var command = new UpdateAdvertismentCommand(adId, Title: newTitle);

            // Act
            var result = _handler.Handle(command, Token).Await();

            // Assert
            result.IsError.ShouldBeTrue();
            result.FirstError.ShouldBe(AdvertismentErrors.Unauthorized);
        }

        [Fact]
        public void Handle_WhenCategoryNotExists_Should_ReturnError()
        {
            // Arrange
            var adId = _testAdId;
            var newCategoryId = Guid.Parse("371d48a3-1176-4267-99a4-6d477c29097e");
            var command = new UpdateAdvertismentCommand(adId, CategoryId: newCategoryId);

            // Act
            var result = _handler.Handle(command, Token).Await();

            // Assert
            result.IsError.ShouldBeTrue();
            result.FirstError.ShouldBe(CategoryErrors.NotFound);
        }

        [Fact]
        public void Handle_WhenTypeNotExists_Should_ReturnError()
        {
            // Arrange
            var adId = _testAdId;
            var newTypeId = Guid.Parse("8a3b3f96-1b32-4750-a427-b40560f404b6");
            var command = new UpdateAdvertismentCommand(adId, AdvertismentTypeId: newTypeId);

            // Act
            var result = _handler.Handle(command, Token).Await();

            // Assert
            result.IsError.ShouldBeTrue();
            result.FirstError.ShouldBe(AdvertismentTypeErrors.NotFound);
        }

        [Fact]
        public void Handle_WhenAdvertismentNotFound_Should_ReturnError()
        {
            // Arrange
            var adId = Guid.Parse("25c4fc5e-008f-4975-9e45-bd93b5232b9a");
            string newTitle = "Новое имя объявления";
            var command = new UpdateAdvertismentCommand(adId, Title: newTitle);

            // Act
            var result = _handler.Handle(command, Token).Await();

            // Assert
            result.IsError.ShouldBeTrue();
            result.FirstError.ShouldBe(AdvertismentErrors.NotFound);
        }
    }
}
