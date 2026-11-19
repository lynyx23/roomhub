using RoomHub.Api.Domain;
using RoomHub.Api.Infrastructure.Persistence;

namespace RoomHub.Api.Features.GetRoom;

public sealed class GetRoomHandler(RoomRepository rooms)
{
    public Result<Room> Handle(Guid id)
    {
        var room = rooms.Get(id);
        return room is null
            ? Result<Room>.Failure(new("RoomNotFound", "The room does not exist.", ErrorType.NotFound))
            : Result<Room>.Success(room);
    }
}
