using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RoomHub.Api.Domain;

namespace RoomHub.Api.Infrastructure.Persistence;

public sealed class RoomHubDbContext(DbContextOptions<RoomHubDbContext> options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoomHubDbContext).Assembly);

    public async Task<IWriteTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        // Microsoft.Data.Sqlite begins a non-deferred (IMMEDIATE) serializable transaction.
        // Reserve the writer before reading: overlap/day-count checks and writes stay atomic,
        // including across separate application instances using the same SQLite file.
        var transaction = await Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);
        return new WriteTransaction(transaction);
    }

    private sealed class WriteTransaction(IDbContextTransaction transaction) : IWriteTransaction
    {
        public Task CommitAsync(CancellationToken cancellationToken = default)
            => transaction.CommitAsync(cancellationToken);
        // Disposing an uncommitted transaction rolls it back.
        public ValueTask DisposeAsync() => transaction.DisposeAsync();
    }
}
