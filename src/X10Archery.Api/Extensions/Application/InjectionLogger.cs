using Serilog;
using X10Archery.Api.Extensions.Logger;

namespace X10Archery.Api.Extensions.Application;

public static class InjectionLogger
{
    // Конфигурация логгера
    public static WebApplicationBuilder ConfigAndAddLogger(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, lc) => lc
            .Enrich.WithCaller()
            .WriteTo.Console()
            .ReadFrom.Configuration(context.Configuration)
        );

        return builder;
    }
}