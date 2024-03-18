using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Light.EmbeddedResources;
using Npgsql;
using WebApp.DatabaseAccess;

namespace WebApp.Contacts.GetContacts;

public sealed class NpgsqlGetContactsSession : AsyncReadOnlyNpgsqlSession, IGetContactsSession
{
    public NpgsqlGetContactsSession(NpgsqlConnection connection) : base(connection) { }

    public async Task<List<ContactListDto>> GetContactsAsync(
        int skip,
        int take,
        CancellationToken cancellationToken = default
    )
    {
        await using var command = await CreateCommandAsync(
            this.GetEmbeddedResource("GetContacts.sql"),
            cancellationToken
        );
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

        return dtoList;
    }
}