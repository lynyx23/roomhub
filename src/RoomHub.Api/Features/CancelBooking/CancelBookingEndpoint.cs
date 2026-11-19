namespace RoomHub.Api.Features.CancelBooking;

public static class CancelBookingEndpoint
{
    public static void MapCancelBookingEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/bookings/{id:guid}/cancel", (Guid id, CancelBookingHandler handler) =>
        {
            handler.Handle(id);
            return TypedResults.NoContent();
        }).WithTags("Bookings");
    }
}
