using Lms.Core.Application.Sessions;
using Lms.Users.Application.Core;
using Lms.Users.Application.Sessions.GetSession;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lms.Core.Api;

public class AuthorizeFilter : IAsyncActionFilter
{
    private const string AuthHeader = "lms-auth-token";
    private readonly ISessionProvider _sessionProvider;
    private readonly IUsersModule _usersModule;

    public AuthorizeFilter(ISessionProvider sessionProvider, IUsersModule usersModule)
    {
        _sessionProvider = sessionProvider;
        _usersModule = usersModule;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var hasAuthHeader = context.HttpContext.Request.Headers.TryGetValue(AuthHeader, out var value);
        if (hasAuthHeader == false)
        {
            context.Result = new UnauthorizedObjectResult("Отсутствует заголовок авторизации");
            return;
        }

        if (value.FirstOrDefault() == null || Guid.TryParse(value.First(), out var sessionId) == false)
        {
            context.Result = new UnauthorizedObjectResult("Некорректный токен авторизации");
            return;
        }

        var query = new GetSessionQuery(sessionId);
        var result = await _usersModule.ExecuteQueryAsync(query);
        if (result.IsFailure || DateTimeOffset.UtcNow.Subtract(result.Value.CreatedAt) > TimeSpan.FromHours(1))
        {
            context.Result = new UnauthorizedObjectResult("Токен авторизации не существует или истек");
            return;
        }
        var session = result.Value;

        _sessionProvider.SetupSession(session.UserId,
            session.OrganizationId,
            session.OrganizationUnitId,
            session.CreatedAt,
            session.UserRole);

        await next();
    }
}