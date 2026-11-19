using RoomHub.Api.Common;
using RoomHub.Api.Features.CreateRoom;
using RoomHub.Api.Features.GetRoom;
using RoomHub.Api.Features.CreateBooking;
using RoomHub.Api.Features.GetBooking;
using RoomHub.Api.Features.CancelBooking;
using RoomHub.Api.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<RoomRepository>();
builder.Services.AddSingleton<BookingRepository>();
builder.Services.AddScoped<CreateRoomHandler>();
builder.Services.AddScoped<GetRoomHandler>();
builder.Services.AddScoped<CreateBookingHandler>();
builder.Services.AddScoped<GetBookingHandler>();
builder.Services.AddScoped<CancelBookingHandler>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.MapGet("/", () => "RoomHub API is running.");
app.MapCreateRoomEndpoint();
app.MapGetRoomEndpoint();
app.MapCreateBookingEndpoint();
app.MapGetBookingEndpoint();
app.MapCancelBookingEndpoint();

app.Run();
