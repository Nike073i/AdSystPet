using Moq;
using AdSyst.Advertisments.Application.Advertisments.Commands.RestoreAdvertisment;
using AdSyst.Advertisments.Application.Tests.Advertisments.Shared;
using AdSyst.Advertisments.Application.Tests.Infrastructure;
using AdSyst.Advertisments.Domain.Advertisments;

namespace AdSyst.Advertisments.Application.Tests.Advertisments.Commands
{
    public class RestoreAdvertismentCommandHandlerTests : BaseCommandTest
    {
        private readonly Mock<IAdvertismentRepository> _repositoryMock;
        private readonly RestoreAdvertismentCommandHandler _handler;
        private readonly Guid _testAdId = Guid.Parse("825841be-fd96-410f-a4e3-4f54dcfae805");

        public RestoreAdvertismentCommandHandlerTests()
        {
            var ad = AdvertismentData.CreateAdvertismentWithTestData(
                TestClock,
                authorId: TestUserId
            );
            ad.Archive();

            _repositoryMock = new Mock<IAdvertismentRepository>();
            _repositoryMock
                .Setup(r => r.GetByIdAsync(_testAdId, false, It.IsAny<CancellationToken>()))
                .ReturnsAsync(ad);

            _handler = new RestoreAdvertismentCommandHandler(
                _repositoryMock.Object,
                UnitOfWorkMock.Object,
                UserContextMock.Object
            );
        }

        [Fact]
        public void Handle_Should_ChangeState()
        {
            // Arrange
            var adId = _testAdId;
            var command = new RestoreAdvertismentCommand(adId);

            // Act
            var result = _handler.Handle(command, Token).Await();

            // Assert
            result.IsError.ShouldBeFalse();
            result.Value.ShouldBe(AdvertismentStatus.Moderation);
            UnitOfWorkMock.VerifySaving();
        }

        [Fact]
        public void Handle_WhenUserIsNotAuthenticated_Should_ReturnError()
        {
            // Arrange
            var adId = _testAdId;
            UserContextMock.Reset();
            var command = new RestoreAdvertismentCommand(adId);

            // Act
            var result = _handler.Handle(command, Token).Await();

            // Assert
            result.IsError.ShouldBeTrue();
            result.FirstError.ShouldBe(AdvertismentErrors.Unauthorized);
        }

        [Fact]
        public void Handle_WhenAdvertismentNotFound_Should_ReturnError()
        {
            // Arrange
            var adId = Guid.Parse("1d36ed11-a86d-443e-97ed-8786df4cc47d");
            var command = new RestoreAdvertismentCommand(adId);

            // Act
            var result = _handler.Handle(command, Token).Await();

            // Assert
            result.IsError.ShouldBeTrue();
            result.FirstError.ShouldBe(AdvertismentErrors.NotFound);
        }

        [Fact]
        public void Handle_WhenAdvertismentSomeoneElse_Should_ReturnError()
        {
            // Arrange
            UserContextMock
                .Setup(c => c.UserId)
                .Returns(Guid.Parse("dd54d890-dfaf-4b26-9ab6-27daea50462a"));
            var adId = _testAdId;
            var command = new RestoreAdvertismentCommand(adId);

            // Act
            var result = _handler.Handle(command, Token).Await();

            // Assert
            result.IsError.ShouldBeTrue();
            result.FirstError.ShouldBe(AdvertismentErrors.Forbidden);
        }

        [Fact]
        public void Handle_WhenAdvertismentInIncorrectState_Should_ReturnError()
        {
            // Arrange
            var adId = _testAdId;
            var ad = AdvertismentData.CreateAdvertismentWithTestData(TestClock);
            _repositoryMock
                .Setup(r => r.GetByIdAsync(adId, false, It.IsAny<CancellationToken>()))
                .ReturnsAsync(ad);
            var command = new RestoreAdvertismentCommand(adId);

            // Act
            var result = _handler.Handle(command, Token).Await();

            // Assert
            result.IsError.ShouldBeTrue();
        }
    }
}
