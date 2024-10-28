using Microsoft.EntityFrameworkCore;
using AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentSystemData;
using AdSyst.Advertisments.Infrastructure.Data.Contexts;

namespace AdSyst.Advertisments.Infrastructure.Advertisments.Services
{
    internal class GetAdvertismentSystemDataService : IGetAdvertismentSystemDataService
    {
        private readonly AdvertismentReadDbContext _dbContext;

        public GetAdvertismentSystemDataService(AdvertismentReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<AdvertismentSystemViewModel?> GetAsync(
            Guid id,
            CancellationToken cancellationToken
        ) =>
            _dbContext
                .Advertisments
                .Where(ad => ad.Id == id)
                .Select(
                    ad => new AdvertismentSystemViewModel(ad.Id, ad.UserId, ad.CreatedAt, ad.Status)
                )
                .FirstOrDefaultAsync(cancellationToken);
    }
}
