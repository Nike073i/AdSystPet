using Calzolari.Grpc.AspNetCore.Validation;
using AdSyst.Advertisments.Api.Grpc.Validators;

namespace AdSyst.Advertisments.Api.Grpc.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGrpcServices(this IServiceCollection services)
        {
            services.AddGrpc(options => options.EnableMessageValidation());
            services.AddRequestValidators();
            services.AddGrpcValidation();
            return services;
        }

        private static IServiceCollection AddRequestValidators(this IServiceCollection services)
        {
            services.AddValidator<GetAdvertismentDetailsRequestValidator>();
            services.AddValidator<GetAdvertismentSystemDataByIdRequestValidator>();
            return services;
        }
    }
}
