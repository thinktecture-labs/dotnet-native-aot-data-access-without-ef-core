using Microsoft.Extensions.DependencyInjection;

namespace WebApp.Contacts.UpdateContact;

public static class UpdateContactModule
{
    public static IServiceCollection AddUpdateContactModule(this IServiceCollection services) =>
        services.AddSingleton<UpdateContactDtoValidator>();
}