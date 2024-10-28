using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using AdSyst.AuthService.EfContext.UserData.Contexts;

namespace AdSyst.AuthService.Api.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private const string Environment = "Development";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment(Environment);
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<UserDataDbContext>)
                );
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<UserDataDbContext>(
                    o => o.UseInMemoryDatabase(Guid.NewGuid().ToString())
                );
            });
        }
    }
}
