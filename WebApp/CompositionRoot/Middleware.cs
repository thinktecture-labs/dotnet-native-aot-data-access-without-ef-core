using Microsoft.AspNetCore.Builder;
using Serilog;
using WebApp.Contacts;

namespace WebApp.CompositionRoot;

public static class Middleware
{
    public static WebApplication ConfigureMiddleware(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseRouting();
        app.MapHealthChecks("/");
        app.MapContactEndpoints();
        return app;
    }
}