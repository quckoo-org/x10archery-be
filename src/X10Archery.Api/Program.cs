using Serilog;
using X10Archery.Api.Extensions.Application;
using X10Archery.Api.Extensions.Infra;
using X10Archery.Api.Extensions.Security;

var builder = WebApplication
    .CreateBuilder(new WebApplicationOptions
    {
        Args = args
    })
    .ConfigAndAddLogger()
    .ConfigAndAddKestrel()
    //.AddPostgres()
    .ConfigureCorsPolicy()
    .InjectServiceCollection()
    .ConfigAndAddGrpc()
    .ConfigureMetaInfo();

var app = builder.Build();

// Лог с информацией о среде выполнения
Log
    .ForContext("Environment", app.Environment.EnvironmentName)
    .Debug("App activated in [{Environment}] mode", app.Environment.EnvironmentName);

app.UseForwardedHeaders();
if (app.Environment.IsProduction()) app.UseHsts();

app.UseRouting();

// Установка зарегистрированной Cors policy
app.UseCors("ClientPermissionCombined");

// Маппинг gRPC сервисов
app.MapGrpcServices();

// Маппинг сервиса gRPC рефлексии
app.MapGrpcReflectionService();

// Маппинг сервиса healthcheck
app.MapHealthChecks("/healthz");

// Маппинг gRPC сервиса healthcheck
app.MapGrpcHealthChecksService();

app.Run();