using RoomHub.Api.Common;

namespace RoomHub.Api.Features.GetBooking;

public static class GetBookingEndpoint
{
    public static void MapGetBookingEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/bookings/{id:guid}", (Guid id, GetBookingHandler handler) =>
        {
            var result = handler.Handle(id);
            if (!result.IsSuccess) return result.Error.ToProblem();
            var booking = result.Value;
            return TypedResults.Ok(new GetBookingResponse(
                booking.Id, booking.RoomId, booking.BookedBy, booking.StartUtc,
                booking.EndUtc, booking.Attendees, booking.Status.ToString()));
        }).WithTags("Bookings");
    }
}
