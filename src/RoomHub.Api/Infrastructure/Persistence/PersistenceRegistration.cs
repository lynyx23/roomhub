using Microsoft.EntityFrameworkCore;
using RoomHub.Api.Domain;

namespace RoomHub.Api.Infrastructure.Persistence;

public static class PersistenceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<RoomHubDbContext>(options => options.UseSqlite(connectionString));
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<RoomHubDbContext>());
        return services;
    }

    public static async Task InitializeDatabaseAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        await using var scope = services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<RoomHubDbContext>();
        await context.Database.MigrateAsync(cancellationToken);
    }
}
