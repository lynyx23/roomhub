using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomHub.Api.Domain;

namespace RoomHub.Api.Infrastructure.Persistence;

public sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedNever();
        builder.Property(b => b.BookedBy).IsRequired();
        builder.Property(b => b.Status).HasConversion<string>().IsRequired();
        builder.Property(b => b.StartUtc).HasConversion(v => v,
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(b => b.EndUtc).HasConversion(v => v,
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.HasOne<Room>().WithMany().HasForeignKey(b => b.RoomId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(b => new { b.RoomId, b.Status, b.StartUtc });
        builder.HasIndex(b => new { b.BookedBy, b.Status, b.StartUtc });
    }
}
