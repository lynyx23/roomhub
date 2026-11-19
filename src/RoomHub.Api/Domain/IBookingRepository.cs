namespace RoomHub.Api.Domain;

public interface IBookingRepository
{
    Task<Booking?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Booking>> ListActiveForValidationAsync(Guid roomId, string? bookedBy,
        CancellationToken cancellationToken = default);
    Task AddAsync(Booking booking, CancellationToken cancellationToken = default);
}
