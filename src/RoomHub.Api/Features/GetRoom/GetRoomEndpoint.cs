namespace RoomHub.Api.Features.GetRoom;

public static class GetRoomEndpoint
{
    public static void MapGetRoomEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/rooms/{id:guid}", (Guid id, GetRoomHandler handler) =>
        {
            var room = handler.Handle(id);
            return TypedResults.Ok(new GetRoomResponse(room.Id, room.Name, room.Capacity));
        }).WithTags("Rooms");
    }
}
