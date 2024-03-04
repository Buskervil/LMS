using Lms.Users.Domain.Employees.ValueObjects;

namespace Lms.Core.Application.Sessions;

public interface ISessionProvider
{
    Guid UserId { get; }
    public Guid OrganizationId { get; }
    public Guid UnitId { get; }
    public DateTimeOffset CreatedAt { get; }
    public UserRole UserRole { get; }

    public void SetupSession(Guid userId, Guid organizationId, Guid unitId, DateTimeOffset createdAt, UserRole role);
}