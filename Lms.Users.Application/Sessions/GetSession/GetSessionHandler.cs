using Lms.Core.Domain.Primitives;
using Lms.Core.Domain.Results;
using Lms.Users.Application.Core;
using Lms.Users.Domain.Sessions;

namespace Lms.Users.Application.Sessions.GetSession;

internal sealed class GetSessionHandler : IQueryHandler<GetSessionQuery, SessionData>
{
    private readonly ISessionRepository _sessionRepository;

    public GetSessionHandler(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public async Task<Result<SessionData>> Handle(GetSessionQuery request, CancellationToken cancellationToken)
    {
        var session = await _sessionRepository.GetById(request.SessionId);
        if (session == null)
        {
            return Result.Failure<SessionData>(ApiError.InternalServerError($"Сессия с id {request.SessionId} не найдна"));
        }

        return new SessionData(session.Id,
            session.EmployeeId,
            session.OrganizationId,
            session.OrganizationUnitId,
            session.CreatedAt,
            session.UserRole);
    }
}