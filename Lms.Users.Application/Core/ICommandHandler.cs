using Lms.Core.Domain.Results;
using MediatR;

namespace Lms.Users.Application.Core
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
        where TCommand : ICommand { }

    public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, Result<TResult>>
        where TCommand : ICommand<TResult>
    { }
}