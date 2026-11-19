using Microsoft.AspNetCore.Http.HttpResults;
using RoomHub.Api.Common;
using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain;

namespace RoomHub.Api.Features.GetRoom;

public static class GetRoomEndpoint
{
    public static void MapGetRoomEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/rooms/{id:guid}", async Task<Results<Ok<GetRoomResponse>, ProblemHttpResult>>
            (Guid id, IQueryHandler<GetRoomQuery, GetRoomResponse?> handler, CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(new(id), cancellationToken);
            return response is null ? RoomErrors.NotFound.ToProblem() : TypedResults.Ok(response);
        }).WithTags("Rooms");
    }
}
