using RoomHub.Api.Domain;

namespace RoomHub.Api.Common;

public static class ErrorHttpMapping
{
    public static IResult ToProblem(this Error error) => Results.Problem(
        title: error.Code, detail: error.Message,
        statusCode: error.Type switch
        {
            ErrorType.Validation => 400,
            ErrorType.NotFound => 404,
            ErrorType.Conflict => 409,
            _ => 500
        });
}
