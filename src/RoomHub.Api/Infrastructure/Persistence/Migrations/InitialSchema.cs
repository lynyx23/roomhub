using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoomHub.Api.Infrastructure.Persistence.Migrations;

[DbContext(typeof(RoomHubDbContext))]
[Migration("20261119000100_InitialSchema")]
public sealed class InitialSchema : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable("Rooms", columns: table => new
        {
            Id = table.Column<Guid>(type: "TEXT", nullable: false),
            Name = table.Column<string>(type: "TEXT", nullable: false),
            Capacity = table.Column<int>(type: "INTEGER", nullable: false)
        }, constraints: table => table.PrimaryKey("PK_Rooms", r => r.Id));

        migrationBuilder.CreateTable("Bookings", columns: table => new
        {
            Id = table.Column<Guid>(type: "TEXT", nullable: false),
            RoomId = table.Column<Guid>(type: "TEXT", nullable: false),
            BookedBy = table.Column<string>(type: "TEXT", nullable: false),
            StartUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
            EndUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
            Attendees = table.Column<int>(type: "INTEGER", nullable: false),
            Status = table.Column<string>(type: "TEXT", nullable: false)
        }, constraints: table =>
        {
            table.PrimaryKey("PK_Bookings", b => b.Id);
            table.ForeignKey("FK_Bookings_Rooms_RoomId", b => b.RoomId, "Rooms", "Id", onDelete: ReferentialAction.Restrict);
        });
        migrationBuilder.CreateIndex("IX_Bookings_RoomId_Status_StartUtc", "Bookings", new[] { "RoomId", "Status", "StartUtc" });
        migrationBuilder.CreateIndex("IX_Bookings_BookedBy_Status_StartUtc", "Bookings", new[] { "BookedBy", "Status", "StartUtc" });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("Bookings");
        migrationBuilder.DropTable("Rooms");
    }

    protected override void BuildTargetModel(ModelBuilder modelBuilder) => InitialSchemaModel.Build(modelBuilder);
}
