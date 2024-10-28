using System.Text.Json.Serialization;
using FastEndpoints;
using FastEndpoints.Swagger;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using AdSyst.Advertisments.Api;
using AdSyst.Advertisments.Api.Grpc;
using AdSyst.Advertisments.Api.Grpc.Extensions;
using AdSyst.Advertisments.Application;
using AdSyst.Advertisments.Infrastructure;
using AdSyst.Common.Presentation.Data;
using AdSyst.Common.Presentation.Middlewares.OperationCanceled;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(configuration));

services.AddApplication();
services.AddInfrastructure(configuration);
services.AddSwagger();
services.AddFastEndpoints();
services.AddAuthentication();
services.AddAuthorization();

services.AddGrpcServices();

var app = builder.Build();
app.InitializeData();

app.UseSerilogRequestLogging();
app.UseAuthentication();
app.UseAuthorization();
app.UseOperationCanceledHandler();
app.MapHealthChecks(
    "/healthz",
    new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse }
);
app.UseFastEndpoints(configure =>
    {
        var enumConverter = new JsonStringEnumConverter();
        configure.Serializer.Options.Converters.Add(enumConverter);
        configure.Serializer.Options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    })
    .UseSwaggerGen();
app.MapGrpcService<AdvertismentServiceServer>();
app.Run();
