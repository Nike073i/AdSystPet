using Microsoft.EntityFrameworkCore;
using AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentDetail;
using AdSyst.Advertisments.Infrastructure.Data.Contexts;

namespace AdSyst.Advertisments.Infrastructure.Advertisments.Services
{
    internal class GetAdvertismentDetailService : IGetAdvertismentDetailService
    {
        private readonly AdvertismentReadDbContext _dbContext;

        public GetAdvertismentDetailService(AdvertismentReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<AdvertismentDetailViewModel?> GetAsync(
            Guid id,
            CancellationToken cancellationToken
        ) =>
            _dbContext
                .Advertisments
                .Where(ad => ad.Id == id)
                .Select(
                    ad =>
                        new AdvertismentDetailViewModel(
                            ad.Id,
                            ad.Title,
                            ad.Description,
                            ad.AdvertismentType!.Id,
                            ad.AdvertismentType.Name,
                            ad.Category!.Id,
                            ad.Category.Name,
                            ad.Price,
                            ad.CreatedAt,
                            ad.Status,
                            ad.UserId,
                            ad.Images!.OrderBy(i => i.Order).Select(i => i.ImageId).ToArray()
                        )
                )
                .FirstOrDefaultAsync(cancellationToken);
    }
}
