using MongoDB.Bson.Serialization;
using AdSyst.Common.DAL.MongoDb.Mappers;
using AdSyst.Moderation.DAL.MongoDb.Models;

namespace AdSyst.Moderation.DAL.MongoDb.Mappers
{
    public class AdvertismentClassMapper : IClassMapper<AdvertismentClassMapper>
    {
        public void Register()
        {
            BsonClassMap.RegisterClassMap<Advertisment>(classMap =>
            {
                classMap.AutoMap();
                classMap.MapIdMember(ad => ad.AdvertismentId);
                classMap.MapProperty(ad => ad.AdvertismentAuthorId);
                classMap.MapCreator(
                    ad => new Advertisment(ad.AdvertismentId, ad.AdvertismentAuthorId)
                );
            });
        }
    }
}
