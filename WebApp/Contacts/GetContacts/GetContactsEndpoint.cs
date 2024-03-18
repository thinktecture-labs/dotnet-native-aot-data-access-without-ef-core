using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Light.EmbeddedResources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Npgsql;
using WebApp.CommonValidation;
using WebApp.DatabaseAccess;

namespace WebApp.Contacts.GetContacts;

public static class GetContactsEndpoint
{
    public static WebApplication MapGetContacts(this WebApplication app)
    {
        app.MapGet("/api/contacts", GetContacts);
        return app;
    }

    public static async Task<IResult> GetContacts(
        NpgsqlConnection npgsqlConnection,
        PagingParametersValidator validator,
        int skip = 0,
        int take = 20,
        CancellationToken cancellationToken = default
    )
    {
        if (validator.CheckForErrors(new PagingParameters(skip, take), out var errors))
        {
            return Results.BadRequest(errors);
        }

        await npgsqlConnection.OpenAsync(cancellationToken);
        await using var command = npgsqlConnection.CreateCommand();
        command.CommandText = typeof(GetContactsEndpoint).GetEmbeddedResource("GetContacts.sql");
        command.Parameters.Add(new NpgsqlParameter<int> { TypedValue = skip });
        command.Parameters.Add(new NpgsqlParameter<int> { TypedValue = take });
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var dtoList = new List<ContactListDto>();
        while (await reader.ReadAsync(cancellationToken))
        {
            var id = reader.GetGuid(0);
            var lastName = reader.GetString(1);
            var firstName = reader.GetString(2);
            var email = reader.GetOptional<string>(3);
            var phone = reader.GetOptional<string>(4);
            dtoList.Add(new ContactListDto(id, lastName, firstName, email, phone));
        }

        return Results.Ok(dtoList);
    }
}