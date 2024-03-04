using Lms.Core.Domain.Results;
using MediatR;

namespace Lms.Users.Application.Core
{
    public sealed class UsersModule : IUsersModule
    {
        private readonly IMediator _mediator;

        public UsersModule(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<Result> ExecuteCommandAsync(ICommand command, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(command, cancellationToken);
        }
        
        public Task<Result<TResult>> ExecuteCommandAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(command, cancellationToken);
        }

        public Task<Result<TResult>> ExecuteQueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(query, cancellationToken);
        }
    }
}