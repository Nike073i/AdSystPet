using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using AdSyst.AuthService.Application.Users.DomainEvents.UserCreated;
using AdSyst.Common.Application;
using AdSyst.Common.BusinessLayer.Options;

namespace AdSyst.AuthService.ApplicationLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services,
            MessageTemplate emailConfirmationMessageTemplate,
            List<Assembly>? assemblies = null
        )
        {
            services.AddTransient<
                IEmailConfirmationMessageContentProvider,
                EmailConfirmationMessageContentProvider
            >(sp => new(emailConfirmationMessageTemplate));

            assemblies ??= new List<Assembly>();
            assemblies.Add(Assembly.GetExecutingAssembly());

            return services.AddApplicationServices(assemblies);
        }
    }
}
