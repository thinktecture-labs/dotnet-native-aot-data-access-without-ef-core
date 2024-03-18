using System;
using System.Threading;
using System.Threading.Tasks;
using Light.EmbeddedResources;
using Npgsql;
using WebApp.DatabaseAccess;
using WebApp.DatabaseAccess.Model;

namespace WebApp.Contacts.UpdateContact;

public sealed class NpgsqlUpdateContactSession : AsyncNpgsqlSession, IUpdateContactSession
{
    public NpgsqlUpdateContactSession(NpgsqlConnection connection) : base(connection) { }

    public async Task<Contact?> GetContactAsync(Guid contactId, CancellationToken cancellationToken = default)
    {
        await using var command = await CreateCommandAsync(
            this.GetEmbeddedResource("GetContact.sql"),
            cancellationToken
        );
        command.Parameters.Add(new NpgsqlParameter<Guid> { TypedValue = contactId });
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        Contact? contact = null;
        if (await reader.ReadAsync(cancellationToken))
        {
            contactId = reader.GetGuid(0);
            var firstName = reader.GetString(1);
            var lastName = reader.GetString(2);
            var email = reader.GetOptional<string>(3);
            var phone = reader.GetOptional<string>(4);
            contact = new Contact
            {
                Id = contactId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone
            };
        }

        return contact;
    }

    public async Task UpdateContactAsync(Contact contact, CancellationToken cancellationToken = default)
    {
        await using var updateCommand = await CreateCommandAsync(
            this.GetEmbeddedResource("UpdateContact.sql"),
            cancellationToken
        );
        updateCommand.Parameters.Add(new NpgsqlParameter<string> { TypedValue = contact.FirstName });
        updateCommand.Parameters.Add(new NpgsqlParameter<string> { TypedValue = contact.LastName });
        updateCommand.Parameters.Add(new NpgsqlParameter<string?> { TypedValue = contact.Email });
        updateCommand.Parameters.Add(new NpgsqlParameter<string?> { TypedValue = contact.Phone });
        updateCommand.Parameters.Add(new NpgsqlParameter<Guid> { TypedValue = contact.Id });
        await updateCommand.ExecuteNonQueryAsync(cancellationToken);
    }
}