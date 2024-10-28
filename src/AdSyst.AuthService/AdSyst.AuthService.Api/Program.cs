using System.Reflection;
using System.Text.Json.Serialization;
using Calzolari.Grpc.AspNetCore.Validation;
using FastEndpoints;
using FastEndpoints.Swagger;
using FluentValidation.AspNetCore;
using Microsoft.FeatureManagement;
using AdSyst.AuthService.Api;
using AdSyst.AuthService.Api.Configs;
using AdSyst.AuthService.Api.Configs.Extensions;
using AdSyst.AuthService.Api.Configs.IdentityServer;
using AdSyst.AuthService.Api.Grpc;
using AdSyst.AuthService.Api.Routing;
using AdSyst.AuthService.Api.Validators;
using AdSyst.AuthService.Application.Routing;
using AdSyst.AuthService.ApplicationLayer;
using AdSyst.AuthService.EfContext.UserData;
using AdSyst.AuthService.SqlServerMigrations;
using AdSyst.Common.Application.Abstractions.Authentication;
using AdSyst.Common.Contracts;
using AdSyst.Common.Infrastructure.Authentication;
using AdSyst.Common.Presentation.Data;
using AdSyst.Common.Presentation.Middlewares.OperationCanceled;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
builder.Host.UseSerilog();

var configurationManager = new ConfigurationHelper(configuration);

services.AddFeatureManagement();
services.AddServicesIdentityServer(configurationManager);
services.AddFluentValidationAutoValidation();
services.AddHttpContextAccessor();
services.AddSwagger();
services.AddScoped<IUserContext, UserContext>();
services.AddScoped<ILinkService, LinkService>();
services.AddFastEndpoints();

var rabbitMqSettins = configurationManager.RabbitMqSettings;

services.ConfigureMassTransit(rabbitMqSettins);

var confirmEmailTemplate = configurationManager.ConfirmEmailTemplate;

var queryHandlersAssemblies = new List<Assembly>
{
    Assembly.GetAssembly(typeof(IUserDataProjectMarker))!
};
services.AddApplication(confirmEmailTemplate, queryHandlersAssemblies);
services.AddInfrastructure();
services.AddGrpc(options => options.EnableMessageValidation());
services.AddValidator<GetUserDataByIdRequestValidator>();
services.AddGrpcValidation();

services.AddLocalApiAuthentication();
services.AddAppHealthChecks();

var app = builder.Build();

app.InitializeData();

app.UseIdentityServer();
app.UseOperationCanceledHandler();
app.MapHealthChecks("/healthz");
app.UseFastEndpoints(configure =>
    {
        var enumConverter = new JsonStringEnumConverter();
        configure.Serializer.Options.Converters.Add(enumConverter);
        configure.Serializer.Options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    })
    .UseSwaggerGen();
app.MapGrpcService<UserService>();
app.Run();

public partial class Program { }
