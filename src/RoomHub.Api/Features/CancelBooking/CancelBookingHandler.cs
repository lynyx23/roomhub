using RoomHub.Api.Domain;
using RoomHub.Api.Infrastructure.Persistence;

namespace RoomHub.Api.Features.CancelBooking;

public sealed class CancelBookingHandler(BookingRepository bookings, TimeProvider timeProvider)
{
    public Result<Unit> Handle(Guid id) => bookings.ExecuteLocked(() =>
    {
        var booking = bookings.Get(id);
        return booking is null
            ? Result<Unit>.Failure(new("BookingNotFound", "The booking does not exist.", ErrorType.NotFound))
            : booking.Cancel(timeProvider.GetUtcNow().UtcDateTime);
    });
}
