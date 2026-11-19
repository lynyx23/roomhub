namespace RoomHub.Api.Domain;

public static class BookingErrors
{
    public static readonly Error NotFound = new("BookingNotFound", "The booking does not exist.", ErrorType.NotFound);
    public static readonly Error AlreadyCancelled = new("BookingAlreadyCancelled", "The booking is already cancelled.", ErrorType.Conflict);
    public static readonly Error CancellationTooLate = new("CancellationTooLate", "A booking can only be cancelled at least 60 minutes before it starts.", ErrorType.Conflict);
}
