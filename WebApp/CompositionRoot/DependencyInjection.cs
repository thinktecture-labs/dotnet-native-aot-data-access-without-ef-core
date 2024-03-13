using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WebApp.JsonAccess;

namespace WebApp.CompositionRoot;

public static class DependencyInjection
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder, ILogger logger)
    {
        builder.UseSerilog(logger);
        builder
           .Services
           .AddJsonSerializationContext()
           .AddHealthChecks();
        
        return builder;
    }
}