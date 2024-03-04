using Lms.Core.Domain.Results;
using MediatR;

namespace Lms.Users.Application.Core
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
        where TQuery : IQuery<TResult>
    { }
}