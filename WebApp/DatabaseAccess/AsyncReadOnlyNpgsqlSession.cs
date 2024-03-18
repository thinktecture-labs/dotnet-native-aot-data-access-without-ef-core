using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;

namespace WebApp.DatabaseAccess;

public abstract class AsyncReadOnlyNpgsqlSession : IAsyncReadOnlySession
{
    protected AsyncReadOnlyNpgsqlSession(NpgsqlConnection connection, IsolationLevel? transactionLevel = null)
    {
        Connection = connection;
        TransactionLevel = transactionLevel;
    }

    protected NpgsqlConnection Connection { get; }
    protected IsolationLevel? TransactionLevel { get; }
    protected NpgsqlTransaction? Transaction { get; private set; }

    private bool IsInitialized
    {
        get
        {
            if (TransactionLevel.HasValue && Transaction is null)
            {
                return false;
            }

            return Connection.State is ConnectionState.Open;
        }
    }

    public virtual async ValueTask DisposeAsync()
    {
        if (Transaction is not null)
        {
            await Transaction.DisposeAsync();
        }

        await Connection.DisposeAsync();
    }

    public virtual void Dispose()
    {
        Transaction?.Dispose();
        Connection.Dispose();
    }

    public ValueTask<NpgsqlCommand> CreateCommandAsync(
        string? sql = null,
        CancellationToken cancellationToken = default
    )
    {
        return IsInitialized ?
            new ValueTask<NpgsqlCommand>(CreateCommand(sql)) :
            InitializeAndCreateCommandAsync(sql, cancellationToken);
    }

    public ValueTask<NpgsqlBatch> CreateBatchAsync(CancellationToken cancellationToken = default)
    {
        return IsInitialized ?
            new ValueTask<NpgsqlBatch>(CreateBatch()) :
            InitializeAndCreateBatchAsync(cancellationToken);
    }

    private async ValueTask<NpgsqlCommand> InitializeAndCreateCommandAsync(
        string? sql,
        CancellationToken cancellationToken
    )
    {
        await InitializeAsync(cancellationToken);
        return CreateCommand(sql);
    }

    private async ValueTask<NpgsqlBatch> InitializeAndCreateBatchAsync(CancellationToken cancellationToken)
    {
        await InitializeAsync(cancellationToken);
        return CreateBatch();
    }

    private async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (Connection.State != ConnectionState.Open)
        {
            await Connection.OpenAsync(cancellationToken);
        }

        if (TransactionLevel.HasValue && Transaction is null)
        {
            Transaction = await Connection.BeginTransactionAsync(cancellationToken);
        }
    }

    private NpgsqlCommand CreateCommand(string? sql)
    {
        var command = Connection.CreateCommand();
        command.Transaction = Transaction;
        if (sql is not null)
        {
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
        }

        return command;
    }

    private NpgsqlBatch CreateBatch()
    {
        var batch = Connection.CreateBatch();
        batch.Transaction = Transaction;
        return batch;
    }
}