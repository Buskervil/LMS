using Lms.Core.Domain.Primitives;
using Lms.Core.Domain.Results;
using Lms.Users.Application.Core;
using Lms.Users.Domain.Employees;
using Lms.Users.Domain.Employees.ValueObjects;
using Lms.Users.Domain.Sessions;

namespace Lms.Users.Application.Sessions.LoginByPassword;

// ReSharper disable once UnusedType.Global
internal sealed class LoginByPasswordCommandHandler : ICommandHandler<LoginByPasswordCommand, Guid>
{
    private readonly IAuthenticationLoginRepository _authenticationLoginRepository;
    private readonly ISessionRepository _sessionRepository;

    public LoginByPasswordCommandHandler(IAuthenticationLoginRepository authenticationLoginRepository, ISessionRepository sessionRepository)
    {
        _authenticationLoginRepository = authenticationLoginRepository;
        _sessionRepository = sessionRepository;
    }

    public async Task<Result<Guid>> Handle(LoginByPasswordCommand request, CancellationToken cancellationToken)
    {
        var login = Login.Create(request.Login);
        var authenticationLogin = await _authenticationLoginRepository.GetByLogin(login);
        if (authenticationLogin == null)
        {
            return Result.Failure<Guid>(ApiError.BadRequest($"Пользователь с логином {request.Login} не зарегистрирован"));
        }

        var session = Session.Create(authenticationLogin, request.Password);
        if (session.IsSuccess)
        {
            _sessionRepository.Add(session.Value);
        }

        return session.Value.Id;
    }
}