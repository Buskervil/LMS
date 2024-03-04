using Lms.Users.Application.Core;
using Lms.Users.Infrastructure.DataAccess;
using MediatR;

namespace Lms.Users.Infrastructure.Processing
{
    public sealed class UnitOfWorkCommandPipelineBehavior<T, TResult> : IPipelineBehavior<T, TResult>
        where T : ICommandRequest<TResult>
    {
        private readonly UsersContext _usersContext;

        public UnitOfWorkCommandPipelineBehavior(UsersContext usersContext)
        {
            _usersContext = usersContext;
        }

        public async Task<TResult> Handle(T request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
        {
            var result = await next();

            await _usersContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}