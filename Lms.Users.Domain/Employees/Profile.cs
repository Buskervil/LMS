using Lms.Core.Domain.Primitives;

namespace Lms.Users.Domain.Employees;

public sealed class Profile : Entity
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Contact { get; private set; }

    private Profile(Guid id, string firstName, string lastName, string contact)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Contact = contact;
    }

    private Profile()
    {
    }

    public static Profile Create(string firstName, string lastName, string contact)
    {
        return new Profile(Guid.NewGuid(), firstName, lastName, contact);
    }

    #region Overrides

    public override bool Equals(Entity other)
    {
        var otherProfile = other as Profile;

        return otherProfile != null && otherProfile.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    #endregion
}