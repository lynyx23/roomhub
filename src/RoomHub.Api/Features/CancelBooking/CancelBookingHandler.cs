using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain;

namespace RoomHub.Api.Features.CancelBooking;

public sealed class CancelBookingHandler(IBookingRepository bookings, IUnitOfWork unitOfWork, TimeProvider timeProvider)
    : ICommandHandler<CancelBookingCommand, Unit>
{
    public async Task<Result<Unit>> HandleAsync(CancelBookingCommand command, CancellationToken cancellationToken = default)
    {
        await using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        var booking = await bookings.GetAsync(command.Id, cancellationToken);
        if (booking is null) return Result<Unit>.Failure(BookingErrors.NotFound);
        var result = booking.Cancel(timeProvider.GetUtcNow().UtcDateTime);
        if (!result.IsSuccess) return result;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return result;
    }
}
