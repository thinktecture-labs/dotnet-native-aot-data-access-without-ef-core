using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Light.EmbeddedResources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Npgsql;
using WebApp.CommonValidation;
using WebApp.DatabaseAccess;
using WebApp.DatabaseAccess.Model;

namespace WebApp.Contacts.UpdateContact;

public static class UpdateContactEndpoint
{
    public static WebApplication MapUpdateContact(this WebApplication app)
    {
        app.MapPut("/api/contacts", UpdateContact);
        return app;
    }

    public static async Task<IResult> UpdateContact(
        NpgsqlConnection npgsqlConnection,
        UpdateContactDtoValidator validator,
        UpdateContactDto dto,
        CancellationToken cancellationToken = default
    )
    {
        if (validator.CheckForErrors(dto, out var errors))
        {
            return Results.BadRequest(errors);
        }

        await npgsqlConnection.OpenAsync(cancellationToken);
        await using var transaction =
            await npgsqlConnection.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
        Contact? existingContact = null;
        await using (var queryCommand = npgsqlConnection.CreateCommand())
        {
            queryCommand.CommandText = typeof(UpdateContactEndpoint).GetEmbeddedResource("GetContact.sql");
            queryCommand.Parameters.Add(new NpgsqlParameter<Guid> { TypedValue = dto.Id });
            await using var reader = await queryCommand.ExecuteReaderAsync(
                CommandBehavior.SingleResult | CommandBehavior.SingleRow,
                cancellationToken
            );
            if (await reader.ReadAsync(cancellationToken))
            {
                var contactId = reader.GetGuid(0);
                var firstName = reader.GetString(1);
                var lastName = reader.GetString(2);
                var email = reader.GetOptional<string>(3);
                var phone = reader.GetOptional<string>(4);
                existingContact = new Contact
                {
                    Id = contactId,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Phone = phone
                };
            }
        }

        if (existingContact is null)
        {
            return Results.NotFound();
        }

        await using var updateCommand = npgsqlConnection.CreateCommand();
        updateCommand.CommandText = typeof(UpdateContactEndpoint).GetEmbeddedResource("UpdateContact.sql");
        updateCommand.Parameters.Add(new NpgsqlParameter<string> { TypedValue = dto.FirstName });
        updateCommand.Parameters.Add(new NpgsqlParameter<string> { TypedValue = dto.LastName });
        updateCommand.Parameters.Add(new NpgsqlParameter<string?> { TypedValue = dto.Email });
        updateCommand.Parameters.Add(new NpgsqlParameter<string?> { TypedValue = dto.Phone });
        updateCommand.Parameters.Add(new NpgsqlParameter<Guid> { TypedValue = dto.Id });
        await updateCommand.ExecuteNonQueryAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return Results.NoContent();
    }
}