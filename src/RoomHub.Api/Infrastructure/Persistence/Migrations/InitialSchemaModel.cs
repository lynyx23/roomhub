using Microsoft.EntityFrameworkCore;

namespace RoomHub.Api.Infrastructure.Persistence.Migrations;

// Frozen initial model: independent of the application's evolving entity configurations.
internal static class InitialSchemaModel
{
    internal static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("ProductVersion", "10.0.14");
        modelBuilder.Entity("RoomHub.Api.Domain.Room", b =>
        {
            b.Property<Guid>("Id").ValueGeneratedNever().HasColumnType("TEXT");
            b.Property<string>("Name").IsRequired().HasColumnType("TEXT");
            b.Property<int>("Capacity").HasColumnType("INTEGER");
            b.HasKey("Id");
            b.ToTable("Rooms");
        });
        modelBuilder.Entity("RoomHub.Api.Domain.Booking", b =>
        {
            b.Property<Guid>("Id").ValueGeneratedNever().HasColumnType("TEXT");
            b.Property<Guid>("RoomId").HasColumnType("TEXT");
            b.Property<string>("BookedBy").IsRequired().HasColumnType("TEXT");
            b.Property<DateTime>("StartUtc").HasColumnType("TEXT");
            b.Property<DateTime>("EndUtc").HasColumnType("TEXT");
            b.Property<int>("Attendees").HasColumnType("INTEGER");
            b.Property<string>("Status").IsRequired().HasColumnType("TEXT");
            b.HasKey("Id");
            b.HasIndex("RoomId", "Status", "StartUtc");
            b.HasIndex("BookedBy", "Status", "StartUtc");
            b.ToTable("Bookings");
            b.HasOne("RoomHub.Api.Domain.Room", null).WithMany().HasForeignKey("RoomId")
                .OnDelete(DeleteBehavior.Restrict).IsRequired();
        });
    }
}
