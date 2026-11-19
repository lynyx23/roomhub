namespace RoomHub.Api.Domain;

public sealed class Room
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int Capacity { get; private set; }
    private Room() { }

    public static Result<Room> Create(string? name, int capacity)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<Room>.Failure(RoomErrors.InvalidName);
        if (capacity <= 0)
            return Result<Room>.Failure(RoomErrors.InvalidCapacity);
        return Result<Room>.Success(new Room { Id = Guid.NewGuid(), Name = name.Trim(), Capacity = capacity });
    }
}
