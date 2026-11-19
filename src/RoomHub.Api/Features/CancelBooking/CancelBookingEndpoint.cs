using RoomHub.Api.Common;

namespace RoomHub.Api.Features.CancelBooking;

public static class CancelBookingEndpoint
{
    public static void MapCancelBookingEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/bookings/{id:guid}/cancel", (Guid id, CancelBookingHandler handler) =>
        {
            var result = handler.Handle(id);
            if (!result.IsSuccess) return result.Error.ToProblem();
            return TypedResults.NoContent();
        }).WithTags("Bookings");
    }
}
