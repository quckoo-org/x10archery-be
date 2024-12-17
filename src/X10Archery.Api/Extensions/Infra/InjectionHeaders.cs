using Microsoft.AspNetCore.HttpOverrides;

namespace X10Archery.Api.Extensions.Infra;

public static class InjectionHeaders
{
    public static WebApplicationBuilder ConfigureMetaInfo(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });

        return builder;
    }
}