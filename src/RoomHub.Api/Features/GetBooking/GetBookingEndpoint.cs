namespace RoomHub.Api.Features.GetBooking;

public static class GetBookingEndpoint
{
    public static void MapGetBookingEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/bookings/{id:guid}", (Guid id, GetBookingHandler handler) =>
        {
            var booking = handler.Handle(id);
            return TypedResults.Ok(new GetBookingResponse(
                booking.Id, booking.RoomId, booking.BookedBy, booking.StartUtc,
                booking.EndUtc, booking.Attendees, booking.Status.ToString()));
        }).WithTags("Bookings");
    }
}
