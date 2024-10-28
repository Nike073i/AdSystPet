using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Advertisments.Domain.AdvertismentTypes;
using AdSyst.Advertisments.Domain.Categories;

namespace AdSyst.Advertisments.Application.Tests.Advertisments.Shared
{
    public class AdvertismentData
    {
        public const string Title = "Заголовок объявления";
        public const string Description = "Описание объявления";
        public static readonly AdvertismentType Type = new("Тип объявления") { Id = Guid.Parse("678844bf-b697-42ef-93ad-f49210738bf5") };
        public static readonly Category Category = new("Категория") { Id = Guid.Parse("b7754e27-c549-441e-be19-e1aa3e295b60") };
        public const decimal Price = 100.12m;
        public static readonly Guid AuthorId = Guid.Parse("57f0426c-b171-4287-9add-012a3084b441");
        public static readonly Guid[] Images =
        {
            Guid.Parse("d0666830-1a98-4dc3-81f0-e95a714c57d9"),
            Guid.Parse("f0308aae-be03-4581-9f29-dc509e96e560"),
            Guid.Parse("ff513092-53d0-4858-925f-e80201d6439c")
        };

        public static Advertisment CreateAdvertismentWithTestData(
            Func<DateTimeOffset> clock,
            string? title = null,
            string? description = null,
            Guid? typeId = null,
            Guid? categoryId = null,
            decimal? price = null,
            Guid? authorId = null,
            Guid[]? images = null
        ) =>
            Advertisment.Create(
                title ?? Title,
                description ?? Description,
                typeId ?? Type.Id,
                categoryId ?? Category.Id,
                price ?? Price,
                authorId ?? AuthorId,
                images ?? Images,
                clock
            );
    }
}
