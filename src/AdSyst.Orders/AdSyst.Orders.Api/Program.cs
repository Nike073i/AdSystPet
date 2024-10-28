using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AdSyst.Common.Presentation.Extensions;
using AdSyst.Orders.Api.Config;
using AdSyst.Orders.Api.Config.Extensions;
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

services
    .AddControllers()
    .AddJsonOptions(configure =>
    {
        var enumConverter = new JsonStringEnumConverter();
        configure.JsonSerializerOptions.Converters.Add(enumConverter);
        configure.JsonSerializerOptions.DefaultIgnoreCondition =
            JsonIgnoreCondition.WhenWritingNull;
    });

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options => options.CustomSchemaIds(s => s.FullName!.Replace("+", ".")));
services.AddApplicationServices(configurationHelper);
services.AddAppAuthorization();
services.AddMongoIdRouteConstraint();
services.AddAppHealthChecks(configurationHelper);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapHealthChecks("/healthz");
app.MapControllers();

app.Run();
