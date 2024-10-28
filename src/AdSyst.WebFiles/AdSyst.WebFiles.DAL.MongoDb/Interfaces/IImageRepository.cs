using AdSyst.WebFiles.DAL.MongoDb.Models;

namespace AdSyst.WebFiles.DAL.MongoDb.Interfaces
{
    public interface IImageRepository
    {
        Task<Image?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task AddAsync(Image image, CancellationToken cancellationToken = default);
    }
}
