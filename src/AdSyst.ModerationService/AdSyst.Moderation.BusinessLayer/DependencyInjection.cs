using Microsoft.Extensions.DependencyInjection;
using AdSyst.Moderation.BusinessLayer.Interfaces;
using AdSyst.Moderation.BusinessLayer.Services;

namespace AdSyst.Moderation.BusinessLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddModerationBusinessLayer(
            this IServiceCollection services
        ) => services.AddTransient<ICorrectionService, CorrectionService>();
    }
}
