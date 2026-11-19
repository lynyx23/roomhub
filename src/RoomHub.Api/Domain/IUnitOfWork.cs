namespace RoomHub.Api.Domain;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IWriteTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}

public interface IWriteTransaction : IAsyncDisposable
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}
