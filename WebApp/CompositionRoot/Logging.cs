using Microsoft.AspNetCore.Builder;
using Serilog;

namespace WebApp.CompositionRoot;

public static class Logging
{
    public static ILogger CreateLogger() =>
        new LoggerConfiguration()
           .MinimumLevel.Information()
           .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
           .WriteTo.Console()
           .CreateLogger();

    public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder, ILogger logger)
    {
        builder.Host.UseSerilog(logger);
        return builder;
    }
}