using FastEndpoints.Swagger;
using AdSyst.AuthService.Api.OpenApi;

namespace AdSyst.AuthService.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.ConfigureOptions<SwaggerOptionsSetup>();
            services.SwaggerDocument(o => o.AutoTagPathSegmentIndex = 2);
            return services;
        }
    }
}
