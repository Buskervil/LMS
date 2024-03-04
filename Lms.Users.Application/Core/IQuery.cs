using Lms.Core.Domain.Results;
using MediatR;

namespace Lms.Users.Application.Core
{
    public interface IQuery<TResult> : IRequest<Result<TResult>> { }
}