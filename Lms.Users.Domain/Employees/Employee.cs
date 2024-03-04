using Lms.Core.Domain.Primitives;
using Lms.Users.Domain.Employees.ValueObjects;

namespace Lms.Users.Domain.Employees;

public sealed class Employee : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid OrganizationId { get; private set; }
    public Guid UnitId { get; private set; }
    public string Post { get; private set; }
    public UserRole Role { get; private set; }
    public Profile Profile { get; private set; }

    private Employee(Guid id,
        Guid organizationId,
        Guid unitId,
        string post,
        UserRole role,
        Profile profile)
    {
        Id = id;
        OrganizationId = organizationId;
        UnitId = unitId;
        Post = post;
        Role = role;
        Profile = profile;
    }

    private Employee()
    {
    }

    public static (Employee Employee, AuthenticationLogin Login) Create(Guid organizationId,
        Guid unitId,
        string post,
        UserRole role,
        Profile profile, Login login)
    {
        var employee = new Employee(Guid.NewGuid(), organizationId, unitId, post, role, profile);
        var authentication = AuthenticationLogin.Create(login, employee);

        return (employee, authentication);
    }

    #region Overrides

    public override bool Equals(Entity other)
    {
        var otherUser = other as Employee;

        return otherUser != null && otherUser.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    #endregion
}