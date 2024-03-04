using Lms.Users.Domain.Employees.ValueObjects;

namespace Lms.Users.Domain.Employees;

public interface IAuthenticationLoginRepository
{
    Task<AuthenticationLogin?> GetByLogin(Login login);
}