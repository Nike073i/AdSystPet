using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AdSyst.Common.Application.Abstractions.Authentication;
using AdSyst.Common.Application.Abstractions.Caching;
using AdSyst.Common.Application.Abstractions.Clock;
using AdSyst.Common.Infrastructure.Authentication;
using AdSyst.Common.Infrastructure.Caching;
using AdSyst.Common.Infrastructure.Clock;
using AdSyst.Common.Presentation.Options;

namespace AdSyst.Common.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCaching(
            this IServiceCollection services,
            RedisSettings redisSettings
        )
        {
            services.AddStackExchangeRedisCache(
                options => options.Configuration = redisSettings.ConnectionString
            );
            services.AddSingleton<ICacheService, CacheService>();
            return services;
        }

        public static IServiceCollection AddClock(this IServiceCollection services) =>
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        public static IServiceCollection AddAuthentication(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var jwtAuthOptions = configuration
                .GetRequiredSection(nameof(JwtAuthOptions))
                .Get<JwtAuthOptions>()!;
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = jwtAuthOptions.Authority;
                    options.Audience = jwtAuthOptions.Audience;
                    options.RequireHttpsMetadata = jwtAuthOptions.RequireHttpsMedatada;
                });
            services.AddHttpContextAccessor();
            services.AddScoped<IUserContext, UserContext>();
            return services;
        }
    }
}
