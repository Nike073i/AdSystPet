using Microsoft.Extensions.DependencyInjection;
using AdSyst.Notifications.BusinessLayer.Interfaces;
using AdSyst.Notifications.BusinessLayer.Services;

namespace AdSyst.Notifications.BusinessLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddNotificationBusinessLayer(
            this IServiceCollection services
        )
        {
            services.AddTransient<INotificationSettingsService, NotificationSettingsService>();
            services.AddTransient<INotifyManager, NotifyManager>();
            return services;
        }
    }
}
