using Lms.Core.Application.Sessions;

namespace Lms.Core.Infrastructure;

public class SessionProvider : ISessionProvider
{
    public Guid UserId { get; init; } = Guid.NewGuid();
}