using RoomHub.Api.Common.CQRS;

namespace RoomHub.Api.Features.GetBooking;

public sealed record GetBookingQuery(Guid Id) : IQuery<GetBookingResponse?>;
