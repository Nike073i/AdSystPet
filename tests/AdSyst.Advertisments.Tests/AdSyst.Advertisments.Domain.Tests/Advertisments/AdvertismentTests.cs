using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Advertisments.Domain.Advertisments.Events;
using AdSyst.Advertisments.Domain.Tests.Infrastructure;

namespace AdSyst.Advertisments.Domain.Tests.Advertisments
{
    public class AdvertismentTests : BaseTest
    {
        private static DateTimeOffset TestClock() => new(2000, 1, 1, 13, 24, 25, new(1, 0, 0));

        [Fact]
        public void Create_Should_SetPropertyValues()
        {
            // Act
            var ad = AdvertismentData.CreateAdvertismentWithTestData(TestClock);

            // Assert
            ad.ShouldNotBeNull();
            ad.Title.ShouldBe(AdvertismentData.Title);
            ad.Description.ShouldBe(AdvertismentData.Description);
            ad.AdvertismentTypeId.ShouldBe(AdvertismentData.Type.Id);
            ad.CategoryId.ShouldBe(AdvertismentData.Category.Id);
            ad.Price.ShouldBe(AdvertismentData.Price);
            ad.Images.ShouldNotBeEmpty();
            ad.Images.Select(i => i.ImageId).SequenceEqual(AdvertismentData.Images).ShouldBeTrue();
            ad.Images.Select(i => i.AdvertismentId).All(id => id == ad.Id).ShouldBeTrue();
            ad.Status.ShouldBe(AdvertismentStatus.Moderation);
            ad.CreatedAt.ShouldBe(TestClock());
        }

        [Fact]
        public void Create_Should_RaiseAdvertismentCreatedDomainEvent()
        {
            // Act
            var ad = AdvertismentData.CreateAdvertismentWithTestData(TestClock);

            // Assert
            ad.ShouldNotBeNull();
            ShouldBeDomainEvent<AdvertismentCreatedDomainEvent>(ad);
        }

        [Fact]
        public void Archive_Should_RaiseAdvertismentStatusChangedDomainEvent()
        {
            // Arrange
            var ad = AdvertismentData.CreateAdvertismentWithTestData(TestClock);

            // Act
            ad.Archive();

            // Assert
            ShouldBeDomainEvent<AdvertismentStatusChangedDomainEvent>(ad);
        }

        [Fact]
        public void Restore_Should_RaiseAdvertismentStatusChangedDomainEvent()
        {
            // Arrange
            var ad = AdvertismentData.CreateAdvertismentWithTestData(TestClock);
            ad.Archive();
            ad.ClearEvents();

            // Act
            ad.Restore();

            // Assert
            ShouldBeDomainEvent<AdvertismentStatusChangedDomainEvent>(ad);
        }

        [Fact]
        public void Approve_Should_RaiseAdvertismentStatusChangedDomainEvent()
        {
            // Arrange
            var ad = AdvertismentData.CreateAdvertismentWithTestData(TestClock);
            ad.ClearEvents();

            // Act
            ad.Approve();

            // Assert
            ShouldBeDomainEvent<AdvertismentStatusChangedDomainEvent>(ad);
        }

        [Fact]
        public void Reject_Should_RaiseAdvertismentStatusChangedDomainEvent()
        {
            // Arrange
            var ad = AdvertismentData.CreateAdvertismentWithTestData(TestClock);
            ad.ClearEvents();

            // Act
            ad.Reject();

            // Assert
            ShouldBeDomainEvent<AdvertismentStatusChangedDomainEvent>(ad);
        }

        [Fact]
        public void Update_Should_RaiseAdvertismentUpdatedDomainEvent()
        {
            // Arrange
            var ad = AdvertismentData.CreateAdvertismentWithTestData(TestClock);
            ad.ClearEvents();
            string newTitle = "Новый заголовок для объявления";

            // Act
            ad.Update(title: newTitle, null, null, null, null, null);

            // Assert
            ShouldBeDomainEvent<AdvertismentUpdatedDomainEvent>(ad);
        }

        [Fact]
        public void Update_Should_SetPropertyValues()
        {
            // Arrange
            string newTitle = "Новый заголовок для объявления";
            string newDescription = "Новое описание для объявления";
            decimal newPrice = 555.5m;
            var newCategoryId = Guid.Parse("b3d3cf1a-6453-4c21-b60e-06335ce5b209");
            var newTypeId = Guid.Parse("06dceb8d-7e53-41b9-9f4c-b4850d9e3ac5");
            Guid[] newImages =
            {
                Guid.Parse("b6f225d7-65f2-4b25-9bae-b24afede04de"),
                Guid.Parse("5d3a1d16-8d85-45d2-8f61-d3f5f77e8e13"),
                Guid.Parse("6fdbf1ef-ad36-4449-aeda-13f362472d95")
            };

            var ad = AdvertismentData.CreateAdvertismentWithTestData(TestClock);

            // Act
            var result = ad.Update(
                newTitle,
                newDescription,
                newPrice,
                newTypeId,
                newCategoryId,
                newImages
            );

            // Assert
            result.IsError.ShouldBeFalse();
            ad.Title.ShouldBe(newTitle);
            ad.Description.ShouldBe(newDescription);
            ad.Price.ShouldBe(newPrice);
            ad.AdvertismentTypeId.ShouldBe(newTypeId);
            ad.CategoryId.ShouldBe(newCategoryId);
            ad.Images!.Select(image => image.ImageId).ShouldBe(newImages);
            ad.Images!.All(a => a.AdvertismentId == ad.Id).ShouldBeTrue();
        }
    }
}
