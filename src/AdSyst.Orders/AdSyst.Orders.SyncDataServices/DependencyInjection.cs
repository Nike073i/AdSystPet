using Microsoft.Extensions.DependencyInjection;
using AdSyst.Advertisments.Api.Grpc;
using AdSyst.Orders.SyncDataServices.Advertisments.Interfaces;
using AdSyst.Orders.SyncDataServices.Advertisments.Services;
using AdSyst.Orders.SyncDataServices.Options;

namespace AdSyst.Orders.SyncDataServices
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSyncDataServices(
            this IServiceCollection services,
            GrpcClientSettings authClientSettings
        )
        {
            services.AddGrpcClient<AdvertismentService.AdvertismentServiceClient>(
                o => o.Address = authClientSettings.Address
            );
            services.AddTransient<IAdvertismentServiceClient, GrpcAdvertismentServiceClient>();
            return services;
        }
    }
}
