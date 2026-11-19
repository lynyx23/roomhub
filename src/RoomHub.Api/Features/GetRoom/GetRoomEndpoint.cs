using RoomHub.Api.Common;

namespace RoomHub.Api.Features.GetRoom;

public static class GetRoomEndpoint
{
    public static void MapGetRoomEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/rooms/{id:guid}", (Guid id, GetRoomHandler handler) =>
        {
            var result = handler.Handle(id);
            if (!result.IsSuccess) return result.Error.ToProblem();
            var room = result.Value;
            return TypedResults.Ok(new GetRoomResponse(room.Id, room.Name, room.Capacity));
        }).WithTags("Rooms");
    }
}
