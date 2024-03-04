using Lms.Users.Application.Core;

namespace Lms.Users.Application.Sessions.GetSession;

public sealed class GetSessionQuery : IQuery<SessionData>
{
    public Guid SessionId { get; }
    
    public GetSessionQuery(Guid sessionId)
    {
        SessionId = sessionId;
    }
}