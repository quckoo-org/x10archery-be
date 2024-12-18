namespace X10Archery.Api.Extensions.Security;

public static class InjectionSecurity
{
    // Конфигурация Cors policy
    public static WebApplicationBuilder ConfigureCorsPolicy(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("ClientPermissionCombined", policy =>
            {
                policy
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .SetIsOriginAllowed(_ => true)
                    .WithExposedHeaders("Content-Type", "Authorization", "Access-Control-Allow-Headers");
            });
        });

        return builder;
    }
}