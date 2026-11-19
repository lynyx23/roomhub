using Microsoft.AspNetCore.Http.HttpResults;
using RoomHub.Api.Common;
using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain;

namespace RoomHub.Api.Features.GetBooking;

public static class GetBookingEndpoint
{
    public static void MapGetBookingEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/bookings/{id:guid}", async Task<Results<Ok<GetBookingResponse>, ProblemHttpResult>>
            (Guid id, IQueryHandler<GetBookingQuery, GetBookingResponse?> handler, CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(new(id), cancellationToken);
            return response is null ? BookingErrors.NotFound.ToProblem() : TypedResults.Ok(response);
        }).WithTags("Bookings");
    }
}
