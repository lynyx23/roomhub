namespace RoomHub.Api.Domain;

public interface IRoomRepository
{
    Task<Room?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Room room, CancellationToken cancellationToken = default);
}
