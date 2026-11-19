using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace RoomHub.Api.Infrastructure.Persistence.Migrations;

[DbContext(typeof(RoomHubDbContext))]
public sealed class RoomHubDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder) => InitialSchemaModel.Build(modelBuilder);
}
