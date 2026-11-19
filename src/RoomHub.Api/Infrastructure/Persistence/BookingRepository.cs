using RoomHub.Api.Domain;

namespace RoomHub.Api.Infrastructure.Persistence;

// Starter: in-memory data. It is lost when the application restarts.
public class BookingRepository
{
    private readonly List<Booking> _bookings = new();
    private readonly object _lock = new();

    public T ExecuteLocked<T>(Func<T> action)
    {
        lock (_lock) return action();
    }

    public List<Booking> ListAll()
    {
        lock (_lock) return _bookings.ToList();
    }

    public void Add(Booking booking)
    {
        lock (_lock)
        {
            _bookings.Add(booking);
        }
    }

    public Booking? Get(Guid id)
    {
        lock (_lock)
        {
            return _bookings.FirstOrDefault(b => b.Id == id);
        }
    }

    public List<Booking> ListForRoom(Guid roomId)
    {
        lock (_lock)
        {
            return _bookings.Where(b => b.RoomId == roomId).ToList();
        }
    }
}
