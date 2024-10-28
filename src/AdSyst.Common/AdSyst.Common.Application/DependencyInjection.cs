using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using AdSyst.Common.Application.Abstractions.Behaviors;

namespace AdSyst.Common.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            List<Assembly> assemblies
        )
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(assemblies.ToArray());
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });
            services.AddValidatorsFromAssemblies(assemblies);
            return services;
        }

        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            Assembly assembly
        ) => services.AddApplicationServices(new List<Assembly> { assembly });
    }
}
