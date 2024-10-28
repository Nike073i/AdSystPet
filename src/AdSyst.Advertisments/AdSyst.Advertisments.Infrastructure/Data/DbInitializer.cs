using Microsoft.EntityFrameworkCore;
using AdSyst.Advertisments.Infrastructure.Data.Contexts;
using AdSyst.Common.Presentation.Data;

namespace AdSyst.Advertisments.Infrastructure.Data
{
    internal class DbInitializer : IDbInitializer
    {
        private readonly AdvertismentDbContext _advertismentDbContext;

        public DbInitializer(AdvertismentDbContext advertismentDbContext)
        {
            _advertismentDbContext = advertismentDbContext;
        }

        public bool Initialize()
        {
            _advertismentDbContext.Database.Migrate();
            return true;
        }
    }
}
