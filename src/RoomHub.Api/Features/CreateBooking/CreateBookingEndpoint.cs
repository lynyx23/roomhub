using RoomHub.Api.Common;

namespace RoomHub.Api.Features.CreateBooking;

public static class CreateBookingEndpoint
{
    public static void MapCreateBookingEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/rooms/{roomId:guid}/bookings", (Guid roomId, CreateBookingRequest request, CreateBookingHandler handler) =>
        {
            var result = handler.Handle(
                roomId, request.BookedBy, request.StartUtc, request.EndUtc, request.Attendees);
            if (!result.IsSuccess) return result.Error.ToProblem();
            var booking = result.Value;
            return TypedResults.Created(
                $"/api/bookings/{booking.Id}",
                new CreateBookingResponse(booking.Id, booking.RoomId, booking.Status.ToString()));
        }).WithTags("Rooms");
    }
}
