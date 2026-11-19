using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain;

namespace RoomHub.Api.Features.CreateRoom;

public sealed record CreateRoomCommand(string? Name, int Capacity) : ICommand<CreateRoomResponse>;
