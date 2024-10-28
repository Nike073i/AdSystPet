using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.StaticFiles;
using AdSyst.WebFiles.Application;
using AdSyst.WebFiles.BusinessLayer;
using AdSyst.WebFiles.DAL.MongoDb;

namespace AdSyst.WebFiles.Api.Config.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            ConfigurationHelper configurationHelper
        )
        {
            var fileStorageSettings = configurationHelper.FileStorageSettings;
            var imageCompressSettings = configurationHelper.ImageCompressSettings;
            services.AddSingleton(fileStorageSettings);
            services.AddSingleton(imageCompressSettings);

            services.AddTransient<IContentTypeProvider, FileExtensionContentTypeProvider>();

            var mongoSettings = configurationHelper.MongoSettings;

            services.AddWebFilesData(mongoSettings);
            services.AddWebFilesServices();
            services.AddWebFilesApplication();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
