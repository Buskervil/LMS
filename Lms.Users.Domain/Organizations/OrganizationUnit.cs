using Lms.Core.Domain.Primitives;
using Lms.Courses.Domain.Courses.ValueObjects;
using Lms.Users.Domain.Employees;

namespace Lms.Users.Domain.Organizations;

public sealed class OrganizationUnit : Entity
{
    private List<Employee> _employees = new();

    public Guid Id { get; private set; }
    public Guid OrganizationId { get; private set; }
    public EntityName EntityName { get; private set; }

    public IReadOnlyCollection<Employee> Employees => _employees.AsReadOnly();

    private OrganizationUnit(Guid id, Guid organizationId, EntityName entityName)
    {
        Id = id;
        OrganizationId = organizationId;
        EntityName = entityName;
    }

    private OrganizationUnit()
    {
    }

    public static OrganizationUnit Create(Guid organizationId, EntityName entityName)
    {
        return new OrganizationUnit(Guid.NewGuid(), organizationId, entityName);
    }

    #region Overrides

    public override bool Equals(Entity other)
    {
        var otherOrganizationUnit = other as OrganizationUnit;

        return otherOrganizationUnit != null && otherOrganizationUnit.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    #endregion

    internal void AddEmployee(Employee employee)
    {
        _employees.Add(employee);
    }
}