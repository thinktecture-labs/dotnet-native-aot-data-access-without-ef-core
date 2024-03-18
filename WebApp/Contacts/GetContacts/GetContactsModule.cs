using Microsoft.Extensions.DependencyInjection;

namespace WebApp.Contacts.GetContacts;

public static class GetContactsModule
{
    public static IServiceCollection AddGetContactsModule(this IServiceCollection services) =>
        services
           .AddSingleton<PagingParametersValidator>()
           .AddScoped<IGetContactsSession, NpgsqlGetContactsSession>();
}