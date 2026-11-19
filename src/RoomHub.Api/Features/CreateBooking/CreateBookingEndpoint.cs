using Microsoft.AspNetCore.Http.HttpResults;
using RoomHub.Api.Common;
using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain;

namespace RoomHub.Api.Features.CreateBooking;

public static class CreateBookingEndpoint
{
    public static void MapCreateBookingEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/rooms/{roomId:guid}/bookings", async Task<Results<Created<CreateBookingResponse>, ProblemHttpResult>>
            (Guid roomId, CreateBookingRequest request, ICommandHandler<CreateBookingCommand, CreateBookingResponse> handler, CancellationToken cancellationToken) =>
        {
            var result = await handler.HandleAsync(new(roomId, request.BookedBy, request.StartUtc, request.EndUtc, request.Attendees), cancellationToken);
            if (!result.IsSuccess) return result.Error.ToProblem();
            return TypedResults.Created($"/api/bookings/{result.Value.Id}", result.Value);
        }).WithTags("Rooms");
    }
}
