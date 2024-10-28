using MongoDB.Driver;
using AdSyst.WebFiles.DAL.MongoDb.Interfaces;
using AdSyst.WebFiles.DAL.MongoDb.Models;

namespace AdSyst.WebFiles.DAL.MongoDb.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IMongoCollection<Image> _images;

        public ImageRepository(IMongoCollection<Image> collection)
        {
            _images = collection;
        }

        public Task AddAsync(Image image, CancellationToken cancellationToken) =>
            _images.InsertOneAsync(image, cancellationToken: cancellationToken);

        public async Task<Image?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var cursor = await _images.FindAsync(
                image => image.Id == id,
                cancellationToken: cancellationToken
            );
            return await cursor.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}
