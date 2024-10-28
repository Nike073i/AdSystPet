using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using AdSyst.Common.Presentation.Constraints;

namespace AdSyst.Common.Presentation.Extensions
{
    public static class RouteOptionsExtensions
    {
        public static IServiceCollection AddMongoIdRouteConstraint(
            this IServiceCollection services
        ) =>
            services.Configure<RouteOptions>(
                options =>
                    options
                        .ConstraintMap
                        .Add(MongoIdConstraint.MongoIdConstraintName, typeof(MongoIdConstraint))
            );
    }
}
