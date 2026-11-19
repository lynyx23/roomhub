using RoomHub.Api.Common;
using RoomHub.Api.Domain;
using RoomHub.Api.Infrastructure.Persistence;

namespace RoomHub.Api.Features.CancelBooking;

public sealed class CancelBookingHandler(BookingRepository bookings)
{
    public void Handle(Guid id)
    {
        var booking = bookings.Get(id);
        if (booking is null)
            throw new BusinessException("BookingNotFound", "The booking does not exist.", 404);

        booking.Status = BookingStatus.Cancelled;
    }
}
