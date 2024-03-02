using Lms.Core.Domain.Results;
using MediatR;

namespace Courses.Application.Core
{
    public interface ICommandRequest<out TResult> : IRequest<TResult> {}
    public interface ICommand : ICommandRequest<Result> { }
    public interface ICommand<TResult> : ICommandRequest<Result<TResult>> { }
}