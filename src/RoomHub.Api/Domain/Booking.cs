namespace RoomHub.Api.Domain;

public enum BookingStatus { Active, Cancelled }

public sealed class Booking
{
    public const int MaxDailyActiveBookings = 2;
    public static readonly TimeSpan MinimumCancellationNotice = TimeSpan.FromMinutes(60);
    public static readonly TimeSpan MaxDuration = TimeSpan.FromHours(4);
    public static readonly TimeSpan LargeRoomMaxDuration = TimeSpan.FromHours(2);
    public const int LargeRoomCapacityThreshold = 10;

    public Guid Id { get; private set; }
    public Guid RoomId { get; private set; }
    public string BookedBy { get; private set; } = string.Empty;
    public DateTime StartUtc { get; private set; }
    public DateTime EndUtc { get; private set; }
    public int Attendees { get; private set; }
    public BookingStatus Status { get; private set; }
    private Booking() { }

    public static Result<Booking> Create(Room room, string? bookedBy, DateTime startUtc,
        DateTime endUtc, int attendees, DateTime nowUtc, IEnumerable<Booking> existingBookings)
    {
        if (string.IsNullOrWhiteSpace(bookedBy))
            return Fail("InvalidBookedBy", "BookedBy is required.");
        // Inputs without a timezone are interpreted as UTC; offset inputs are normalized.
        startUtc = NormalizeUtc(startUtc);
        endUtc = NormalizeUtc(endUtc);
        if (endUtc <= startUtc)
            return Fail("InvalidRange", "The end must be after the start.");
        if (attendees <= 0)
            return Fail("InvalidAttendees", "Attendees must be positive.");
        if (attendees > room.Capacity)
            return Fail("AttendeesExceedCapacity", "The room does not have enough seats.");
        if (startUtc < nowUtc)
            return Fail("BookingInPast", "A booking cannot start in the past.");
        var maxDuration = room.Capacity > LargeRoomCapacityThreshold ? LargeRoomMaxDuration : MaxDuration;
        if (endUtc - startUtc > maxDuration)
            return Fail("BookingTooLong", $"The maximum booking duration is {maxDuration.TotalHours} hours.");

        var participant = bookedBy.Trim();
        var active = existingBookings.Where(b => b.Status == BookingStatus.Active).ToList();
        if (active.Any(b => b.RoomId == room.Id && b.StartUtc < endUtc && startUtc < b.EndUtc))
            return Fail("RoomOccupied", "The room is occupied in this interval.", ErrorType.Conflict);
        if (active.Count(b => b.BookedBy == participant && b.StartUtc.Date == startUtc.Date) >= MaxDailyActiveBookings)
            return Fail("DailyBookingLimit", "A participant can have at most two active bookings per UTC day.", ErrorType.Conflict);

        return Result<Booking>.Success(new Booking
        {
            Id = Guid.NewGuid(), RoomId = room.Id, BookedBy = participant,
            StartUtc = startUtc, EndUtc = endUtc, Attendees = attendees, Status = BookingStatus.Active
        });
    }

    public Result<Unit> Cancel(DateTime nowUtc)
    {
        if (Status == BookingStatus.Cancelled)
            return Result<Unit>.Failure(BookingErrors.AlreadyCancelled);
        if (StartUtc - nowUtc < MinimumCancellationNotice)
            return Result<Unit>.Failure(BookingErrors.CancellationTooLate);
        Status = BookingStatus.Cancelled;
        return Result<Unit>.Success(new Unit());
    }

    private static DateTime NormalizeUtc(DateTime value) => value.Kind == DateTimeKind.Local
        ? value.ToUniversalTime() : DateTime.SpecifyKind(value, DateTimeKind.Utc);

    private static Result<Booking> Fail(string code, string message, ErrorType type = ErrorType.Validation)
        => Result<Booking>.Failure(new(code, message, type));
}
