using Microsoft.EntityFrameworkCore;
using AdSyst.Advertisments.Application.AdvertismentTypes.Queries.GetAdvertismentTypeById;
using AdSyst.Advertisments.Infrastructure.Data.Contexts;

namespace AdSyst.Advertisments.Infrastructure.AdvertismentTypes.Services
{
    internal class GetAdvertismentTypeService : IGetAdvertismentTypeService
    {
        private readonly AdvertismentReadDbContext _dbContext;

        public GetAdvertismentTypeService(AdvertismentReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<AdvertismentTypeViewModel?> GetAsync(
            Guid id,
            CancellationToken cancellationToken
        ) =>
            _dbContext
                .AdvertismentTypes
                .Where(t => t.Id == id)
                .Select(type => new AdvertismentTypeViewModel(type.Id, type.Name))
                .FirstOrDefaultAsync(cancellationToken);
    }
}
