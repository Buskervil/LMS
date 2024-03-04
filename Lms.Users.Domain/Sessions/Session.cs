using Lms.Core.Domain.Primitives;
using Lms.Core.Domain.Results;
using Lms.Users.Domain.Employees;
using Lms.Users.Domain.Employees.ValueObjects;

namespace Lms.Users.Domain.Sessions;

public sealed class Session : Entity
{
    public Guid Id { get; private set; }
    public Guid EmployeeId { get; private set; }
    public Guid OrganizationId { get; private set; }
    public Guid OrganizationUnitId { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public UserRole UserRole { get; private set; }

    private Session(Guid id, Guid employeeId, Guid organizationId, Guid organizationUnitId, DateTimeOffset createdAt, UserRole userRole)
    {
        Id = id;
        EmployeeId = employeeId;
        OrganizationId = organizationId;
        OrganizationUnitId = organizationUnitId;
        CreatedAt = createdAt;
        UserRole = userRole;
    }
    
    private Session() {}

    public static Session Create(Guid employeeId,
        Guid organizationId,
        Guid organizationUnitId,
        DateTimeOffset createdAt,
        UserRole userRole)
    {
        return new Session(Guid.NewGuid(), employeeId, organizationId, organizationUnitId, createdAt, userRole);
    }

    #region Overrides

    public override bool Equals(Entity other)
    {
        var otherSession = other as Session;

        return otherSession != null && otherSession.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    #endregion

    public static Result<Session> Create(AuthenticationLogin authenticationLogin, string password)
    {
        var result = authenticationLogin.ValidatePassword(password);

        if (result.IsFailure)
        {
            return Result.Failure<Session>(result.Error);
        }
        
        return new Session(Guid.NewGuid(),
            authenticationLogin.Employee.Id,
            authenticationLogin.Employee.OrganizationId,
            authenticationLogin.Employee.UnitId,
            DateTimeOffset.UtcNow,
            authenticationLogin.Employee.Role);
    }
}