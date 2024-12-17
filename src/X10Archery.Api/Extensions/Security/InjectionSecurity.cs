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
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(_ => true)
                    .AllowCredentials();
            });
        });

        return builder;
    }
}