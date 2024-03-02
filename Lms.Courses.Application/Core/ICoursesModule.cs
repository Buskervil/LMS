using Lms.Core.Domain.Results;

namespace Courses.Application.Core
{
    public interface ICoursesModule
    {
        Task<Result> ExecuteCommandAsync(ICommand command, CancellationToken cancellationToken = default);
        Task<Result<TResult>> ExecuteCommandAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);
        Task<Result<TResult>> ExecuteQueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
    }
}