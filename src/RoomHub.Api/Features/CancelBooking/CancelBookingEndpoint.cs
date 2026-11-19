using Microsoft.AspNetCore.Http.HttpResults;
using RoomHub.Api.Common;
using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain;

namespace RoomHub.Api.Features.CancelBooking;

public static class CancelBookingEndpoint
{
    public static void MapCancelBookingEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/bookings/{id:guid}/cancel", async Task<Results<NoContent, ProblemHttpResult>>
            (Guid id, ICommandHandler<CancelBookingCommand, Unit> handler, CancellationToken cancellationToken) =>
        {
            var result = await handler.HandleAsync(new(id), cancellationToken);
            if (!result.IsSuccess) return result.Error.ToProblem();
            return TypedResults.NoContent();
        }).WithTags("Bookings");
    }
}
