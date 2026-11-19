using RoomHub.Api.Common;

namespace RoomHub.Api.Features.CreateRoom;

public static class CreateRoomEndpoint
{
    public static void MapCreateRoomEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/rooms/", (CreateRoomRequest request, CreateRoomHandler handler) =>
        {
            var result = handler.Handle(request.Name, request.Capacity);
            if (!result.IsSuccess) return result.Error.ToProblem();
            var room = result.Value;
            return TypedResults.Created(
                $"/api/rooms/{room.Id}",
                new CreateRoomResponse(room.Id, room.Name, room.Capacity));
        }).WithTags("Rooms");
    }
}
