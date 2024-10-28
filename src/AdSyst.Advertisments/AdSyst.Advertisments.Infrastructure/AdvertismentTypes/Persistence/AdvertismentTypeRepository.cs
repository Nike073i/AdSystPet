using AdSyst.Advertisments.Domain.AdvertismentTypes;
using AdSyst.Advertisments.Infrastructure.Data.Abstractions;
using AdSyst.Advertisments.Infrastructure.Data.Contexts;

namespace AdSyst.Advertisments.Infrastructure.AdvertismentTypes.Persistence
{
    internal class AdvertismentTypeRepository
        : Repository<AdvertismentType, Guid>,
            IAdvertismentTypeRepository
    {
        public AdvertismentTypeRepository(AdvertismentDbContext dbContext)
            : base(dbContext) { }
    }
}
