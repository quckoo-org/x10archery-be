﻿using Serilog;
using Serilog.Configuration;

namespace X10Archery.Api.Extensions.Logger;

public static class CallerEnrichmentConfiguration
{
    public static LoggerConfiguration WithCaller(this LoggerEnrichmentConfiguration enrichmentConfiguration)
    {
        return enrichmentConfiguration.With<CallerEnricher>();
    }
}