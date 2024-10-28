using Moq;
using AdSyst.Advertisments.Application.Advertisments.Commands.ArchiveAdvertisment;
using AdSyst.Advertisments.Application.Tests.Advertisments.Shared;
using AdSyst.Advertisments.Application.Tests.Infrastructure;
using AdSyst.Advertisments.Domain.Advertisments;

namespace AdSyst.Advertisments.Application.Tests.Advertisments.Commands
{
    public class ArchiveAdvertismentCommandHandlerTests : BaseCommandTest
    {
        private readonly Mock<IAdvertismentRepository> _repositoryMock;
        private readonly ArchiveAdvertismentCommandHandler _handler;
        private readonly Guid _testAdId = Guid.Parse("260f1410-a123-4399-b40d-00a7e2805a70");

        public ArchiveAdvertismentCommandHandlerTests()
        {
            _repositoryMock = new Mock<IAdvertismentRepository>();
            var ad = AdvertismentData.CreateAdvertismentWithTestData(
                TestClock,
                authorId: TestUserId
            );
            _repositoryMock
                .Setup(r => r.GetByIdAsync(_testAdId, false, It.IsAny<CancellationToken>()))
                .ReturnsAsync(ad);

            _handler = new ArchiveAdvertismentCommandHandler(
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
            var command = new ArchiveAdvertismentCommand(adId);

            // Act
            var result = _handler.Handle(command, Token).Await();

            // Assert
            result.IsError.ShouldBeFalse();
            result.Value.ShouldBe(AdvertismentStatus.Archival);
            UnitOfWorkMock.VerifySaving();
        }

        [Fact]
        public void Handle_WhenUserIsNotAuthenticated_Should_ReturnError()
        {
            // Arrange
            var adId = _testAdId;
            UserContextMock.Reset();
            var command = new ArchiveAdvertismentCommand(adId);

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
            var adId = Guid.Parse("ddf70f36-6b75-45b8-8633-975406a4aa40");
            var command = new ArchiveAdvertismentCommand(adId);

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
            var command = new ArchiveAdvertismentCommand(adId);

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
            ad.Archive();
            _repositoryMock
                .Setup(r => r.GetByIdAsync(adId, false, It.IsAny<CancellationToken>()))
                .ReturnsAsync(ad);
            var command = new ArchiveAdvertismentCommand(adId);

            // Act
            var result = _handler.Handle(command, Token).Await();

            // Assert
            result.IsError.ShouldBeTrue();
        }
    }
}
