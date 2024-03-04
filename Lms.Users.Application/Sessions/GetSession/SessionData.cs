using Lms.Users.Domain.Employees.ValueObjects;

namespace Lms.Users.Application.Sessions.GetSession;

public sealed class SessionData
{
    public Guid Id { get; }
    public Guid UserId { get; }
    public Guid OrganizationId { get; }
    public Guid OrganizationUnitId { get; }
    public DateTimeOffset CreatedAt { get; }
    public UserRole UserRole { get; }

    public SessionData(Guid id, Guid userId, Guid organizationId, Guid organizationUnitId, DateTimeOffset createdAt, UserRole userRole)
    {
        Id = id;
        UserId = userId;
        OrganizationId = organizationId;
        OrganizationUnitId = organizationUnitId;
        CreatedAt = createdAt;
        UserRole = userRole;
    }
}