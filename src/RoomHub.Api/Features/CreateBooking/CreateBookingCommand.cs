using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain;

namespace RoomHub.Api.Features.CreateBooking;

public sealed record CreateBookingCommand(Guid RoomId, string? BookedBy, DateTime StartUtc, DateTime EndUtc, int Attendees) : ICommand<CreateBookingResponse>;
