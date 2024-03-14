using System.Threading.Tasks;
using Light.EmbeddedResources;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using WebApp.DatabaseAccess.Precompiled;

namespace WebApp.DatabaseAccess;

public static class DatabaseAccessModule
{
    public static IServiceCollection AddDatabaseAccess(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        var npgsqlDataSource = new NpgsqlDataSourceBuilder(connectionString).Build();
        return services
           .AddSingleton(npgsqlDataSource)
           .AddScoped<NpgsqlConnection>(sp => sp.GetRequiredService<NpgsqlDataSource>().CreateConnection())
           .AddDbContext<DatabaseContext>(
                options => options
                   .UseNpgsql(npgsqlDataSource)
                   .UseModel(DatabaseContextModel.Instance)
            );
    }

    public static async Task SetUpDatabaseAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var connection = scope.ServiceProvider.GetRequiredService<NpgsqlConnection>();
        await connection.OpenAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = typeof(DatabaseAccessModule).GetEmbeddedResource("DatabaseSetup.sql");
        await command.ExecuteNonQueryAsync();
    }
}