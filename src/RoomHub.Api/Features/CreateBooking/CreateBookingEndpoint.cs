namespace RoomHub.Api.Features.CreateBooking;

public static class CreateBookingEndpoint
{
    public static void MapCreateBookingEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/rooms/{roomId:guid}/bookings", (Guid roomId, CreateBookingRequest request, CreateBookingHandler handler) =>
        {
            var booking = handler.Handle(
                roomId, request.BookedBy, request.StartUtc, request.EndUtc, request.Attendees);
            return TypedResults.Created(
                $"/api/bookings/{booking.Id}",
                new CreateBookingResponse(booking.Id, booking.RoomId, booking.Status.ToString()));
        }).WithTags("Rooms");
    }
}
