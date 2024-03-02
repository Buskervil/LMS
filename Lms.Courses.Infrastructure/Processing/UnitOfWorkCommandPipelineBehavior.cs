using Courses.Application.Core;
using Lms.Courses.Infrastructure.DataAccess;
using MediatR;

namespace Lms.Courses.Infrastructure.Processing
{
    public sealed class UnitOfWorkCommandPipelineBehavior<T, TResult> : IPipelineBehavior<T, TResult>
        where T : ICommandRequest<TResult>
    {
        private readonly CoursesContext _unitOfWork;

        public UnitOfWorkCommandPipelineBehavior(CoursesContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResult> Handle(T request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
        {
            var result = await next();

            await _unitOfWork.CommitAsync(cancellationToken);

            return result;
        }
    }
}