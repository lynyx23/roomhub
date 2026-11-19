using RoomHub.Api.Features.CreateRoom;
using RoomHub.Api.Features.GetRoom;
using RoomHub.Api.Features.CreateBooking;
using RoomHub.Api.Features.GetBooking;
using RoomHub.Api.Features.CancelBooking;

namespace RoomHub.Api.Common;

public static class EndpointRegistration
{
    public static void MapRoomHubEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => "RoomHub API is running.");
        app.MapCreateRoomEndpoint();
        app.MapGetRoomEndpoint();
        app.MapCreateBookingEndpoint();
        app.MapGetBookingEndpoint();
        app.MapCancelBookingEndpoint();
    }
}
