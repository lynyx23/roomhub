using RoomHub.Api.Common;
using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddHandlers();
builder.Services.AddPersistence(builder.Configuration.GetConnectionString("RoomHubDB")
    ?? throw new InvalidOperationException("Connection string 'RoomHubDB' is required."));

var app = builder.Build();
await app.Services.InitializeDatabaseAsync();
app.UseStatusCodePages();
app.MapRoomHubEndpoints();
await app.RunAsync();
