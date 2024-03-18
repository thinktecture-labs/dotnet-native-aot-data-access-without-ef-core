using System;
using System.Threading;
using System.Threading.Tasks;
using WebApp.DatabaseAccess;
using WebApp.DatabaseAccess.Model;

namespace WebApp.Contacts.UpdateContact;

public interface IUpdateContactSession : IAsyncSession
{
    Task<Contact?> GetContactAsync(Guid contactId, CancellationToken cancellationToken = default);
    Task UpdateContactAsync(Contact contact, CancellationToken cancellationToken = default);
}