using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain;

namespace RoomHub.Api.Features.CreateRoom;

public sealed class CreateRoomHandler(IRoomRepository rooms, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRoomCommand, CreateRoomResponse>
{
    public async Task<Result<CreateRoomResponse>> HandleAsync(CreateRoomCommand command, CancellationToken cancellationToken = default)
    {
        var result = Room.Create(command.Name, command.Capacity);
        if (!result.IsSuccess) return Result<CreateRoomResponse>.Failure(result.Error);
        var room = result.Value;
        await rooms.AddAsync(room, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<CreateRoomResponse>.Success(new(room.Id, room.Name, room.Capacity));
    }
}
