using RoomHub.Api.Domain;
using RoomHub.Api.Infrastructure.Persistence;

namespace RoomHub.Api.Features.CreateBooking;

public sealed class CreateBookingHandler(RoomRepository rooms, BookingRepository bookings, TimeProvider timeProvider)
{
    public Result<Booking> Handle(Guid roomId, string? bookedBy, DateTime startUtc, DateTime endUtc, int attendees)
    {
        var room = rooms.Get(roomId);
        if (room is null)
            return Result<Booking>.Failure(new("RoomNotFound", "The room does not exist.", ErrorType.NotFound));

        return bookings.ExecuteLocked(() =>
        {
            var result = Booking.Create(room, bookedBy, startUtc, endUtc, attendees,
                timeProvider.GetUtcNow().UtcDateTime, bookings.ListAll());
            if (result.IsSuccess) bookings.Add(result.Value);
            return result;
        });
    }
}
