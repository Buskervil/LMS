using Lms.Core.Application.Sessions;
using Lms.Users.Domain.Employees.ValueObjects;

namespace Lms.Core.Infrastructure;

public class SessionProvider : ISessionProvider
{
    public Guid UserId { get; private set; }
    public Guid OrganizationId { get; private set; }
    public Guid UnitId { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public UserRole UserRole { get; private set; }
    
    public void SetupSession(Guid userId, Guid organizationId, Guid unitId, DateTimeOffset createdAt, UserRole role)
    {
        if (UserId != Guid.Empty)
        {
            throw new Exception("Провайдер сессии уже инициализирован");
        }
        
        UserId = userId;
        OrganizationId = organizationId;
        UnitId = unitId;
        CreatedAt = createdAt;
        UserRole = role;
    }
}