using System;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Serilog;
using WebApp.CompositionRoot;
using WebApp.DatabaseAccess;

[module:DapperAot]

namespace WebApp;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        Log.Logger = Logging.CreateLogger();
        try
        {
            var app = WebApplication
               .CreateSlimBuilder(args)
               .ConfigureServices(Log.Logger)
               .Build()
               .ConfigureMiddleware();

            await app.SetUpDatabaseAsync();
            await app.RunAsync();
            return 0;
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, "Could not run web app");
            return 1;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
}
