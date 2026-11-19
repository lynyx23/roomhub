using Microsoft.EntityFrameworkCore;
using RoomHub.Api.Domain;

namespace RoomHub.Api.Infrastructure.Persistence;

public sealed class BookingRepository(RoomHubDbContext context) : IBookingRepository
{
    public Task<Booking?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => context.Bookings.SingleOrDefaultAsync(b => b.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Booking>> ListActiveForValidationAsync(Guid roomId, string? bookedBy,
        CancellationToken cancellationToken = default)
    {
        var participant = bookedBy?.Trim();
        return await context.Bookings.AsNoTracking()
            .Where(b => b.Status == BookingStatus.Active && (b.RoomId == roomId || b.BookedBy == participant))
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Booking booking, CancellationToken cancellationToken = default)
        => await context.Bookings.AddAsync(booking, cancellationToken);
}
