using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;

namespace X10Archery.Api.Extensions.Application;

public static class InjectionKestrel
{
    // Конфигурация Kestrel
    public static WebApplicationBuilder ConfigAndAddKestrel(this WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureKestrel((_, opt) =>
        {
            var appHost = builder.Configuration.GetValue<string>("App:Host");
            var grpcPort = builder.Configuration.GetValue<int>("App:Ports:Http2");

            opt.Limits.MinRequestBodyDataRate = null;

            opt.Listen(IPAddress.Parse(appHost ?? "0.0.0.0"), grpcPort, listenOptions =>
            {
                Log.Information(
                    "The application [{AppName}] is successfully started at [{StartTime}] (UTC) | protocol gRPC (http2)",
                    AppDomain.CurrentDomain.FriendlyName,
                    DateTime.UtcNow.ToString("F"));

                listenOptions.Protocols = HttpProtocols.Http2;
            });

            opt.AllowAlternateSchemes = true;
        });

        return builder;
    }
}