using Microsoft.AspNetCore.Http.HttpResults;
using RoomHub.Api.Common;
using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain;

namespace RoomHub.Api.Features.CreateRoom;

public static class CreateRoomEndpoint
{
    public static void MapCreateRoomEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/rooms", async Task<Results<Created<CreateRoomResponse>, ProblemHttpResult>>
            (CreateRoomRequest request, ICommandHandler<CreateRoomCommand, CreateRoomResponse> handler, CancellationToken cancellationToken) =>
        {
            var result = await handler.HandleAsync(new(request.Name, request.Capacity), cancellationToken);
            if (!result.IsSuccess) return result.Error.ToProblem();
            return TypedResults.Created($"/api/rooms/{result.Value.Id}", result.Value);
        }).WithTags("Rooms");
    }
}
