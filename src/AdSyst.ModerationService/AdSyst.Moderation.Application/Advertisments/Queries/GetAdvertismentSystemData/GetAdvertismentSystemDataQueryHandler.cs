using ErrorOr;
using MediatR;
using MongoDB.Driver;
using AdSyst.Moderation.DAL.MongoDb.Errors;
using AdSyst.Moderation.DAL.MongoDb.Models;

namespace AdSyst.Moderation.Application.Advertisments.Queries.GetAdvertismentSystemData
{
    public class GetAdvertismentSystemDataQueryHandler
        : IRequestHandler<GetAdvertismentSystemDataQuery, ErrorOr<AdvertismentSystemData>>
    {
        private readonly IMongoCollection<Advertisment> _advertisments;

        public GetAdvertismentSystemDataQueryHandler(IMongoCollection<Advertisment> advertisments)
        {
            _advertisments = advertisments;
        }

        public async Task<ErrorOr<AdvertismentSystemData>> Handle(
            GetAdvertismentSystemDataQuery request,
            CancellationToken cancellationToken
        )
        {
            var projection = Builders<Advertisment>
                .Projection
                .Expression(
                    ad => new AdvertismentSystemData(ad.AdvertismentId, ad.AdvertismentAuthorId)
                );
            var advertisment = await _advertisments
                .Find(ad => ad.AdvertismentId == request.AdvertismentId)
                .Project(projection)
                .FirstOrDefaultAsync(cancellationToken);
            return advertisment is null ? AdvertismentErrors.NotFound : advertisment;
        }
    }
}
