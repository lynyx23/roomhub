using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain;

namespace RoomHub.Api.Features.CreateBooking;

public sealed class CreateBookingHandler(IRoomRepository rooms, IBookingRepository bookings,
    IUnitOfWork unitOfWork, TimeProvider timeProvider)
    : ICommandHandler<CreateBookingCommand, CreateBookingResponse>
{
    public async Task<Result<CreateBookingResponse>> HandleAsync(CreateBookingCommand command, CancellationToken cancellationToken = default)
    {
        await using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        var room = await rooms.GetAsync(command.RoomId, cancellationToken);
        if (room is null) return Result<CreateBookingResponse>.Failure(RoomErrors.NotFound);
        var existing = await bookings.ListActiveForValidationAsync(room.Id, command.BookedBy, cancellationToken);
        var result = Booking.Create(room, command.BookedBy, command.StartUtc, command.EndUtc, command.Attendees,
            timeProvider.GetUtcNow().UtcDateTime, existing);
        if (!result.IsSuccess) return Result<CreateBookingResponse>.Failure(result.Error);
        var booking = result.Value;
        await bookings.AddAsync(booking, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return Result<CreateBookingResponse>.Success(new(booking.Id, booking.RoomId, booking.Status.ToString()));
    }
}
