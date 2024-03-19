using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;

namespace WebApp.DatabaseAccess;

public abstract class AsyncNpgsqlSession : AsyncReadOnlyNpgsqlSession, IAsyncSession
{
    protected AsyncNpgsqlSession(
        NpgsqlConnection connection,
        IsolationLevel transactionLevel = IsolationLevel.ReadCommitted
    )
        : base(connection, transactionLevel) { }

    public virtual Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        Transaction!.CommitAsync(cancellationToken);
}