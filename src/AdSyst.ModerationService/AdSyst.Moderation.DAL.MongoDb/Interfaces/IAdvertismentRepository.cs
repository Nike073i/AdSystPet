using AdSyst.Moderation.DAL.MongoDb.Models;

namespace AdSyst.Moderation.DAL.MongoDb.Interfaces
{
    public interface IAdvertismentRepository
    {
        Task<Advertisment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task CreateAsync(Advertisment advertisment, CancellationToken cancellationToken = default);

        Task UpdateAsync(Advertisment advertisment, CancellationToken cancellationToken = default);
    }
}
