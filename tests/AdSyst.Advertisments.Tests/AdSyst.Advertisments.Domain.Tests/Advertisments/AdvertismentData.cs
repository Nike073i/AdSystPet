using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Advertisments.Domain.AdvertismentTypes;
using AdSyst.Advertisments.Domain.Categories;

namespace AdSyst.Advertisments.Domain.Tests.Advertisments
{
    public class AdvertismentData
    {
        public const string Title = "Заголовок объявления";
        public const string Description = "Описание объявления";
        public static readonly AdvertismentType Type = new("Тип объявления") { Id = Guid.Parse("26625627-7739-477a-b95b-021941f79278") };
        public static readonly Category Category = new("Категория") { Id = Guid.Parse("593eadd6-9c15-4365-8456-f93ff7fb485c") };
        public const decimal Price = 100.12m;
        public static readonly Guid AuthorId = Guid.Parse("57f0426c-b171-4287-9add-012a3084b441");
        public static readonly Guid[] Images =
        {
            Guid.Parse("d0666830-1a98-4dc3-81f0-e95a714c57d9"),
            Guid.Parse("f0308aae-be03-4581-9f29-dc509e96e560"),
            Guid.Parse("ff513092-53d0-4858-925f-e80201d6439c")
        };

        public static Advertisment CreateAdvertismentWithTestData(Func<DateTimeOffset> clock) =>
            Advertisment.Create(
                Title,
                Description,
                Type.Id,
                Category.Id,
                Price,
                AuthorId,
                Images,
                clock
            );
    }
}
