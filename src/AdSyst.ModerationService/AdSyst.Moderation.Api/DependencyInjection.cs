using FastEndpoints.Swagger;
using AdSyst.Moderation.Api.OpenApi;

namespace AdSyst.Moderation.Api
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
