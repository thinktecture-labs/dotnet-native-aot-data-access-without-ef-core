using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApp.DatabaseAccess;

namespace WebApp.Contacts.Common;

public interface IGetContactSession : IAsyncReadOnlySession
{
    Task<List<GetContactRecord>> GetContactWithAddressesAsync(
        Guid contactId,
        CancellationToken cancellationToken = default
    );
}