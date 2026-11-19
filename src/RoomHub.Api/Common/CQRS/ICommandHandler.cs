using RoomHub.Api.Domain;

namespace RoomHub.Api.Common.CQRS;

public interface ICommand<TResult>;

public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand<TResult>
{
    Task<Result<TResult>> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}
