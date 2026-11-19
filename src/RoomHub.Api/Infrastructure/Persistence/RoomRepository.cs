using Microsoft.EntityFrameworkCore;
using RoomHub.Api.Domain;

namespace RoomHub.Api.Infrastructure.Persistence;

public sealed class RoomRepository(RoomHubDbContext context) : IRoomRepository
{
    public Task<Room?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => context.Rooms.SingleOrDefaultAsync(r => r.Id == id, cancellationToken);

    public async Task AddAsync(Room room, CancellationToken cancellationToken = default)
        => await context.Rooms.AddAsync(room, cancellationToken);
}
