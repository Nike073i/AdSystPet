using MongoDB.Driver;
using AdSyst.Moderation.DAL.MongoDb.Interfaces;
using AdSyst.Moderation.DAL.MongoDb.Models;

namespace AdSyst.Moderation.DAL.MongoDb.Repositories
{
    public class AdvertismentRepository : IAdvertismentRepository
    {
        private readonly IMongoCollection<Advertisment> _advertisments;

        public AdvertismentRepository(IMongoCollection<Advertisment> collection)
        {
            _advertisments = collection;
        }

        public Task<Advertisment?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            _advertisments
                .Find(advertisment => advertisment.AdvertismentId == id)
                .FirstOrDefaultAsync(cancellationToken)!;

        public Task CreateAsync(Advertisment advertisment, CancellationToken cancellationToken) =>
            _advertisments.InsertOneAsync(advertisment, null, cancellationToken);

        public Task UpdateAsync(Advertisment advertisment, CancellationToken cancellationToken) =>
            _advertisments.ReplaceOneAsync(
                ad => ad.AdvertismentId == advertisment.AdvertismentId,
                advertisment,
                new ReplaceOptions { IsUpsert = false },
                cancellationToken: cancellationToken
            );
    }
}
