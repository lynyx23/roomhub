using RoomHub.Api.Common.CQRS;

namespace RoomHub.Api.Features.GetRoom;

public sealed record GetRoomQuery(Guid Id) : IQuery<GetRoomResponse?>;
