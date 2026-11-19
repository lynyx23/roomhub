using RoomHub.Api.Domain;
using RoomHub.Api.Infrastructure.Persistence;

namespace RoomHub.Api.Features.CreateRoom;

public sealed class CreateRoomHandler(RoomRepository rooms)
{
    public Result<Room> Handle(string? name, int capacity)
    {
        var result = Room.Create(name, capacity);
        if (result.IsSuccess) rooms.Add(result.Value);
        return result;
    }
}
