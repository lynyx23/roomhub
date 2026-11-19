using RoomHub.Api.Common;
using RoomHub.Api.Domain;
using RoomHub.Api.Infrastructure.Persistence;

namespace RoomHub.Api.Features.GetBooking;

public sealed class GetBookingHandler(BookingRepository bookings)
{
    public Booking Handle(Guid id)
    {
        var booking = bookings.Get(id);
        if (booking is null)
            throw new BusinessException("BookingNotFound", "The booking does not exist.", 404);

        return booking;
    }
}
