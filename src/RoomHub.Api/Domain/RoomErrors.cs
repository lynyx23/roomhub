namespace RoomHub.Api.Domain;

public static class RoomErrors
{
    public static readonly Error NotFound = new("RoomNotFound", "The room does not exist.", ErrorType.NotFound);
    public static readonly Error InvalidName = new("InvalidName", "Room name is required.", ErrorType.Validation);
    public static readonly Error InvalidCapacity = new("InvalidCapacity", "Capacity must be positive.", ErrorType.Validation);
}
