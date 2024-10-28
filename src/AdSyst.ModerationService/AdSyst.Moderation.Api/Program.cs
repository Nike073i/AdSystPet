using System.Text.Json.Serialization;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AdSyst.Common.Application.Abstractions.Authentication;
using AdSyst.Common.Infrastructure;
using AdSyst.Common.Infrastructure.Authentication;
using AdSyst.Moderation.Api;
using AdSyst.Moderation.Api.Config;
using AdSyst.Moderation.Api.Config.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
builder.Host.UseSerilog();

var configurationHelper = new ConfigurationHelper(configuration);

var jwtAuthOptions = configurationHelper.JwtAuthOptions;

services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = jwtAuthOptions.Authority;
        options.Audience = jwtAuthOptions.Audience;
        options.RequireHttpsMetadata = jwtAuthOptions.RequireHttpsMedatada;
    });
services.AddAuthorization();
services.AddHttpContextAccessor();
services.AddScoped<IUserContext, UserContext>();

services.AddFastEndpoints();
services.AddSwagger();

services.AddApplicationServices(configurationHelper);
services.AddAppHealthChecks(configurationHelper);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapHealthChecks("/healthz");
app.UseFastEndpoints(configure =>
    {
        var enumConverter = new JsonStringEnumConverter();
        configure.Serializer.Options.Converters.Add(enumConverter);
        configure.Serializer.Options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    })
    .UseSwaggerGen();

app.Run();
