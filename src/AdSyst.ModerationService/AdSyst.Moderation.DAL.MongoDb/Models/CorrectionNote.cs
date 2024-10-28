using MongoDB.Bson;

namespace AdSyst.Moderation.DAL.MongoDb.Models
{
    public class CorrectionNote
    {
        public ObjectId Id { get; init; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public Guid ModeratorId { get; }

        public bool IsActive { get; set; }

        public DateTimeOffset CreatedAt { get; init; }

        public CorrectionNote(string title, Guid moderatorId)
        {
            Id = ObjectId.GenerateNewId();
            Title = title;
            ModeratorId = moderatorId;
            CreatedAt = DateTimeOffset.Now;
            IsActive = true;
        }
    }
}
