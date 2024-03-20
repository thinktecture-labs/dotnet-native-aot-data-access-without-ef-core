using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Light.EmbeddedResources;
using WebApp.DatabaseAccess;

namespace WebApp.Contacts.Common;

public static class GetContactExtensions
{
    public static async Task<List<GetContactRecord>> GetContactAsync(
        this AsyncReadOnlyNpgsqlSession session,
        Guid contactId,
        CancellationToken cancellationToken = default
    )
    {
        var connection = await session.GetInitializedConnectionAsync(cancellationToken);
        return (
            await connection.QueryAsync<GetContactRecord>(
                typeof(GetContactExtensions).GetEmbeddedResource("GetContact.sql"),
                new { Id = contactId },
                session.Transaction
            )
        ).AsList();
    }

    public static async Task<ContactDetailDto?> GetContactDetailDtoAsync(
        this IGetContactSession session,
        Guid contactId,
        CancellationToken cancellationToken = default
    )
    {
        var records = await session.GetContactWithAddressesAsync(contactId, cancellationToken);
        return records.ConvertToDto();
    }
}