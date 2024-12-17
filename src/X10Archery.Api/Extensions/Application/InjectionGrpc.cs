using System.IO.Compression;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using X10Archery.Api.Services.Grpc.Servers;

namespace X10Archery.Api.Extensions.Application;

public static class InjectionGrpc
{
    // Регистрация и конфигурация gRPC сервиса в коллекции сервисов
    public static WebApplicationBuilder ConfigAndAddGrpc(this WebApplicationBuilder builder)
    {
        builder.Services.AddGrpc(options =>
        {
            // // Промежуточное ПО для работы с Azure (аутентификация)
            // options.Interceptors.Add<AuthInterceptor>();
            // // Промежуточное ПО для работы с Rbac (авторизация)
            // options.Interceptors.Add<RbacInterceptor>();
            // // Промежуточное ПО для работы с логгированием времени запросов
            // options.Interceptors.Add<TimerInterceptor>();

            options.IgnoreUnknownServices = false;
            options.MaxReceiveMessageSize = null;
            options.MaxSendMessageSize = null;
            options.ResponseCompressionLevel = CompressionLevel.Optimal;
            options.ResponseCompressionAlgorithm = "gzip";
            options.EnableDetailedErrors = false;
        });

        // Регистрация и конфигурация состояния gRPC сервиса в коллекции сервисов
        builder.Services.AddGrpcHealthChecks().AddCheck("grpc_health_check", () => HealthCheckResult.Healthy());

        // Регистрация сервсиа gRPC рефлексии в коллекции сервисов
        builder.Services.AddGrpcReflection();

        return builder;
    }

    public static WebApplication MapGrpcServices(this WebApplication app)
    {
        // Маппинг gRPC сервиса для работы с доступом
        app.MapGrpcService<GrpcServerAuthService>();

        return app;
    }
}
