using Microsoft.EntityFrameworkCore;
using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Infrastructure.Persistence;

namespace RoomHub.Api.Features.GetRoom;

public sealed class GetRoomHandler(RoomHubDbContext context) : IQueryHandler<GetRoomQuery, GetRoomResponse?>
{
    public Task<GetRoomResponse?> HandleAsync(GetRoomQuery query, CancellationToken cancellationToken = default)
        => context.Rooms.AsNoTracking().Where(r => r.Id == query.Id)
            .Select(r => new GetRoomResponse(r.Id, r.Name, r.Capacity))
            .SingleOrDefaultAsync(cancellationToken);
}
