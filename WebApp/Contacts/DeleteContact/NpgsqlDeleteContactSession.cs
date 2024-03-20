﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
        await using var batch = await CreateBatchAsync(cancellationToken);
        batch.BatchCommands.Add(CreateBatchCommandWithContactId(DeleteAddressesSql, id));
        batch.BatchCommands.Add(CreateBatchCommandWithContactId(DeleteContactSql, id));
        await batch.ExecuteNonQueryAsync(cancellationToken);
    }

    private static NpgsqlBatchCommand CreateBatchCommandWithContactId(string sql, Guid id)
    {
        var batchCommand = new NpgsqlBatchCommand(sql);
        batchCommand.Parameters.Add(new NpgsqlParameter<Guid> { TypedValue = id });
        return batchCommand;
    }
}