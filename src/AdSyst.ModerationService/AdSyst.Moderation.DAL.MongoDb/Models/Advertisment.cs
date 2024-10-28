namespace AdSyst.Moderation.DAL.MongoDb.Models
{
    public class Advertisment
    {
        public Guid AdvertismentId { get; init; }

        public Guid AdvertismentAuthorId { get; }

        public List<CorrectionNote> CorrectionNotes { get; init; }

        public Advertisment(Guid advertismentId, Guid authorId)
        {
            AdvertismentId = advertismentId;
            AdvertismentAuthorId = authorId;
            CorrectionNotes = new();
        }
    }
}
