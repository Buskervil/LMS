using Lms.Core.Domain.Primitives;
using Lms.Users.Domain.Employees.ValueObjects;

namespace Lms.Users.Domain.Employees;

public sealed class AuthenticationLogin : Entity
{
    public Guid Id { get; private set; }
    public Login Login { get; private set; }
    public Password Password { get; private set; }

    private AuthenticationLogin(Guid id, Login login, Password password)
    {
        Id = id;
        Login = login;
        Password = password;
    }

    public static AuthenticationLogin Create(Login login)
    {
        var password = Password.Create(Guid.NewGuid().ToString());
        return new AuthenticationLogin(Guid.NewGuid(), login, password);
    }

    #region Overrides

    public override bool Equals(Entity other)
    {
        var otherLogin = other as AuthenticationLogin;

        return otherLogin != null && otherLogin.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    #endregion
}