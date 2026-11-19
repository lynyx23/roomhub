using Microsoft.EntityFrameworkCore;
using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Infrastructure.Persistence;

namespace RoomHub.Api.Features.GetBooking;

public sealed class GetBookingHandler(RoomHubDbContext context) : IQueryHandler<GetBookingQuery, GetBookingResponse?>
{
    public Task<GetBookingResponse?> HandleAsync(GetBookingQuery query, CancellationToken cancellationToken = default)
        => context.Bookings.AsNoTracking().Where(r => r.Id == query.Id)
            .Select(r => new GetBookingResponse(r.Id, r.RoomId, r.BookedBy, r.StartUtc, r.EndUtc, r.Attendees, r.Status.ToString()))
            .SingleOrDefaultAsync(cancellationToken);
}
