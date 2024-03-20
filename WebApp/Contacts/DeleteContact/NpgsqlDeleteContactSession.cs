using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Light.EmbeddedResources;
using Npgsql;
using WebApp.Contacts.Common;
using WebApp.DatabaseAccess;

namespace WebApp.Contacts.DeleteContact;

public sealed class NpgsqlDeleteContactSession : AsyncNpgsqlSession, IDeleteContactSession
{
    private static readonly string DeleteAddressesSql =
        typeof(NpgsqlDeleteContactSession).GetEmbeddedResource("DeleteAddresses.sql");

    private static readonly string DeleteContactSql =
        typeof(NpgsqlDeleteContactSession).GetEmbeddedResource("DeleteContact.sql");

    public NpgsqlDeleteContactSession(NpgsqlConnection connection) : base(connection) { }

    public Task<List<GetContactRecord>> GetContactWithAddressesAsync(
        Guid id,
        CancellationToken cancellationToken = default
    ) =>
        this.GetContactAsync(id, cancellationToken);

    public async Task DeleteContactAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var connection = await GetInitializedConnectionAsync(cancellationToken);
        await connection.ExecuteAsync(
            DeleteAddressesSql,
            new { ContactId = id },
            Transaction
        );

        await connection.ExecuteAsync(
            DeleteContactSql,
            new { Id = id },
            Transaction
        );
    }
}