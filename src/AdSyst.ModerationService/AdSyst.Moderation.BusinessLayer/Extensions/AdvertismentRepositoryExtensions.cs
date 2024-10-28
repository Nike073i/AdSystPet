using AdSyst.Common.BusinessLayer.Exceptions;
using AdSyst.Moderation.DAL.MongoDb.Interfaces;
using AdSyst.Moderation.DAL.MongoDb.Models;

namespace AdSyst.Moderation.BusinessLayer.Extensions
{
    public static class AdvertismentRepositoryExtensions
    {
        public static async Task<Advertisment> GetByIdOrThrowAsync(
            this IAdvertismentRepository orderRepository,
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            return await orderRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException(nameof(Advertisment), id);
        }
    }
}
