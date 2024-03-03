using Lms.Core.Domain.Primitives;
using Lms.Core.Domain.Results;
using Lms.Courses.Domain.Courses.ValueObjects;
using Lms.Users.Domain.Employees;
using Lms.Users.Domain.Employees.ValueObjects;

namespace Lms.Users.Domain.Organizations;

public sealed class Organization : AggregateRoot
{
    private List<OrganizationUnit> _units = new();

    public Guid Id { get; private set; }
    public EntityName Name { get; private set; }

    public IReadOnlyCollection<OrganizationUnit> Units => _units.AsReadOnly();

    private Organization()
    {
    }

    private Organization(Guid id, EntityName name, OrganizationUnit unit)
    {
        Id = id;
        Name = name;
        _units.Add(unit);
    }

    public static Organization Create(EntityName name,
        EntityName unitName,
        string employeeFirstName,
        string employeeLastName,
        string employeeContact,
        Login employeeLogin)
    {
        var id = Guid.NewGuid();
        var unit = OrganizationUnit.Create(id, unitName);
        var employeeProfile = Profile.Create(employeeFirstName, employeeLastName, employeeContact);
        var employee = Employee.Create(id, unit.Id, "Управляющий", UserRole.Supervisor, employeeProfile, employeeLogin);

        unit.AddEmployee(employee);

        return new Organization(id, name, unit);
    }

    public Result AddNewEmployee(string firstName, string lastName, string contact, Guid unitId, string post, UserRole role, Login login)
    {
        var unit = _units.FirstOrDefault(u => u.Id == unitId);
        if (unit == null)
        {
            return Result.Failure(ApiError.BadRequest($"Подразделение или филиал с id {unitId} не существует"));
        }
        
        var employeeProfile = Profile.Create(firstName, lastName, contact);
        var employee = Employee.Create(Id, unitId, post, role, employeeProfile, login);

        unit.AddEmployee(employee);
        
        return Result.Success();
    }

    #region Overrides

    public override bool Equals(Entity other)
    {
        var otherOrganization = other as Organization;

        return otherOrganization != null && otherOrganization.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    #endregion
}