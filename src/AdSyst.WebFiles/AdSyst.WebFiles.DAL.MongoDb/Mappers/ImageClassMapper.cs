using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using AdSyst.Common.DAL.MongoDb.Mappers;
using AdSyst.WebFiles.DAL.MongoDb.Enums;
using AdSyst.WebFiles.DAL.MongoDb.Models;

namespace AdSyst.WebFiles.DAL.MongoDb.Mappers
{
    public class ImageClassMapper : IClassMapper<ImageClassMapper>
    {
        public void Register()
        {
            BsonClassMap.RegisterClassMap<Image>(classMap =>
            {
                classMap.AutoMap();
                classMap
                    .MapMember(i => i.Paths)
                    .SetSerializer(
                        new DictionaryInterfaceImplementerSerializer<Dictionary<ImageSize, string>>(
                            DictionaryRepresentation.ArrayOfDocuments
                        )
                    );
                classMap.MapCreator(model => new Image(model.Id));
            });
        }
    }
}
