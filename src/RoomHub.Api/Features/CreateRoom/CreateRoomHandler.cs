using RoomHub.Api.Common;
using RoomHub.Api.Domain;
using RoomHub.Api.Infrastructure.Persistence;

namespace RoomHub.Api.Features.CreateRoom;

public sealed class CreateRoomHandler(RoomRepository rooms)
{
    public Room Handle(string? name, int capacity)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessException("InvalidName", "Room name is required.", 400);

        if (capacity <= 0)
            throw new BusinessException("InvalidCapacity", "Capacity must be positive.", 400);

        var room = new Room
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Capacity = capacity
        };

        rooms.Add(room);
        return room;
    }
}
