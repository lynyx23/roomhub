using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain;

namespace RoomHub.Api.Features.CancelBooking;

public sealed record CancelBookingCommand(Guid Id) : ICommand<Unit>;
