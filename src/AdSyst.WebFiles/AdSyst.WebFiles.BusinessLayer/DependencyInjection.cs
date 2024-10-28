using Microsoft.Extensions.DependencyInjection;
using AdSyst.WebFiles.BusinessLayer.Interfaces;
using AdSyst.WebFiles.BusinessLayer.Services;

namespace AdSyst.WebFiles.BusinessLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebFilesServices(this IServiceCollection services)
        {
            services.AddTransient<IImageProcessor, ImageProcessor>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IImageCompressor, ImageCompressor>();
            services.AddTransient<IImageService, ImageService>();
            return services;
        }
    }
}
