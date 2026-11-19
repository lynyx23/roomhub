namespace RoomHub.Api.Features.CreateRoom;

public record CreateRoomRequest(string? Name, int Capacity);

public record CreateRoomResponse(Guid Id, string Name, int Capacity);
