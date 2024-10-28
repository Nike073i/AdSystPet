using AdSyst.WebFiles.DAL.MongoDb.Enums;

namespace AdSyst.WebFiles.DAL.MongoDb.Models
{
    public class Image
    {
        public Guid Id { get; init; }

        public Dictionary<ImageSize, string> Paths { get; init; }

        public DateTimeOffset CreatedAt { get; init; }

        public Image(Guid id)
        {
            Id = id;
            CreatedAt = DateTimeOffset.Now;
            Paths = new();
        }
    }
}
