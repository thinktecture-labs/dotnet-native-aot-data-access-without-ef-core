﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;
using WebApp.Contacts.Common;
using WebApp.DatabaseAccess;

namespace WebApp.Contacts.GetContact;

public sealed class NpgsqlGetContactSession(NpgsqlConnection connection)
    : AsyncReadOnlyNpgsqlSession(connection), IGetContactSession
{
    public Task<List<GetContactRecord>> GetContactWithAddressesAsync(
        Guid contactId,
        CancellationToken cancellationToken = default
    ) =>
        this.GetContactAsync(contactId, cancellationToken);
}