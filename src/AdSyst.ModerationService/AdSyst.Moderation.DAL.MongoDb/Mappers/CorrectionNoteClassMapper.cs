using MongoDB.Bson.Serialization;
using AdSyst.Common.DAL.MongoDb.Mappers;
using AdSyst.Moderation.DAL.MongoDb.Models;

namespace AdSyst.Moderation.DAL.MongoDb.Mappers
{
    public class CorrectionNoteClassMapper : IClassMapper<CorrectionNoteClassMapper>
    {
        public void Register()
        {
            BsonClassMap.RegisterClassMap<CorrectionNote>(classMap =>
            {
                classMap.AutoMap();
                classMap.MapProperty(note => note.ModeratorId);
                classMap.MapCreator(note => new CorrectionNote(note.Title, note.ModeratorId));
            });
        }
    }
}
