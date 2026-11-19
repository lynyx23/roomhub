using RoomHub.Api.Domain;
using RoomHub.Api.Infrastructure.Persistence;

namespace RoomHub.Api.Features.GetBooking;

public sealed class GetBookingHandler(BookingRepository bookings)
{
    public Result<Booking> Handle(Guid id)
    {
        var booking = bookings.Get(id);
        return booking is null
            ? Result<Booking>.Failure(new("BookingNotFound", "The booking does not exist.", ErrorType.NotFound))
            : Result<Booking>.Success(booking);
    }
}
