using Lms.Core.Domain.Results;
using MediatR;

namespace Lms.Users.Application.Core
{
    public interface ICommandRequest<out TResult> : IRequest<TResult> {}
    public interface ICommand : ICommandRequest<Result> { }
    public interface ICommand<TResult> : ICommandRequest<Result<TResult>> { }
}