using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
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
        var connection = await GetInitializedConnectionAsync(cancellationToken);
        return (
            await connection.QueryAsync<ContactListDto>(
                this.GetEmbeddedResource("GetContacts.sql"),
                new { Skip = skip, Take = take },
                Transaction
            )
        ).AsList();
    }
}