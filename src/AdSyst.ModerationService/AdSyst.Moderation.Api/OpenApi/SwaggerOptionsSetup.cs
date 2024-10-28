using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AdSyst.Moderation.Api.OpenApi
{
    public class SwaggerOptionsSetup : IConfigureOptions<SwaggerGenOptions>
    {
        private static readonly string _xmlFileName =
            $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

        public void Configure(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", OpenApiInfo);

            options.AddSecurityDefinition(
                JwtBearerDefaults.AuthenticationScheme,
                OpenApiSecurityScheme
            );
            options.AddSecurityRequirement(OpenApiSecurityRequirement);
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, _xmlFileName));
        }

        private static OpenApiInfo OpenApiInfo =>
            new()
            {
                Version = "v1",
                Title = "AdSyst ModerationService API",
                Description = "API Сервиса модерации",
                Contact = new() { Name = "Никита", Email = "nike073i@mail.ru" },
            };

        private static OpenApiSecurityScheme OpenApiSecurityScheme =>
            new()
            {
                In = ParameterLocation.Header,
                Description = "Введите JWT токен доступа",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = JwtBearerDefaults.AuthenticationScheme
            };

        private static OpenApiSecurityRequirement OpenApiSecurityRequirement =>
            new()
            {
                {
                    new()
                    {
                        Reference = new() { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                    },
                    new List<string>()
                }
            };
    }
}
