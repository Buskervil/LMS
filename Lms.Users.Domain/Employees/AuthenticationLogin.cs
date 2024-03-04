using Lms.Core.Domain.Primitives;
using Lms.Core.Domain.Results;
using Lms.Users.Domain.Employees.ValueObjects;

namespace Lms.Users.Domain.Employees;

public sealed class AuthenticationLogin : Entity
{
    public Login Login { get; private set; }
    public Password Password { get; private set; }
    public Employee Employee { get; private set; }

    private AuthenticationLogin(Login login, Password password, Employee employee)
    {
        Login = login;
        Password = password;
        Employee = employee;
    }
    
    private AuthenticationLogin() { }

    public static AuthenticationLogin Create(Login login, Employee employee)
    {
        var password = Password.Create(Guid.NewGuid().ToString());
        return new AuthenticationLogin(login, password, employee);
    }
    
    #region Overrides

    public override bool Equals(Entity other)
    {
        var otherLogin = other as AuthenticationLogin;

        return otherLogin != null && otherLogin.Login == Login;
    }

    public override int GetHashCode()
    {
        return Login.GetHashCode();
    }

    #endregion

    public Result ValidatePassword(string password)
    {
        if (Password.Verify(password) == false)
        {
            return Result.Failure(ApiError.Forbidden("Неверный проль"));
        }

        return Result.Success();
    }
}