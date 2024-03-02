using Lms.Core.Domain.Results;
using MediatR;

namespace Courses.Application.Core
{
    public interface IQuery<TResult> : IRequest<Result<TResult>> { }
}