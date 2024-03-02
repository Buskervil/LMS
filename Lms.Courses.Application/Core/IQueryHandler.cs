using Lms.Core.Domain.Results;
using MediatR;

namespace Courses.Application.Core
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
        where TQuery : IQuery<TResult>
    { }
}