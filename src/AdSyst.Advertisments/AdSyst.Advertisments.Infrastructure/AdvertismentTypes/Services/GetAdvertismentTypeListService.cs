using Microsoft.EntityFrameworkCore;
using AdSyst.Advertisments.Application.AdvertismentTypes.Queries.GetAdvertismentTypeList;
using AdSyst.Advertisments.Infrastructure.Data.Contexts;

namespace AdSyst.Advertisments.Infrastructure.AdvertismentTypes.Services
{
    internal class GetAdvertismentTypeListService : IGetAdvertismentTypeListService
    {
        private readonly AdvertismentReadDbContext _dbContext;

        public GetAdvertismentTypeListService(AdvertismentReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<AdvertismentTypeViewModel>> GetAsync(
            CancellationToken cancellationToken
        ) =>
            await _dbContext
                .AdvertismentTypes
                .Select(type => new AdvertismentTypeViewModel(type.Id, type.Name))
                .ToListAsync(cancellationToken);
    }
}
