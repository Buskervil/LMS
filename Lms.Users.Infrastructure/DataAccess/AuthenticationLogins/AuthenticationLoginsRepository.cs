using Lms.Users.Domain.Employees;
using Lms.Users.Domain.Employees.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Lms.Users.Infrastructure.DataAccess.AuthenticationLogins;

public class AuthenticationLoginsRepository : IAuthenticationLoginRepository
{
    private readonly UsersContext _usersContext;

    public AuthenticationLoginsRepository(UsersContext usersContext)
    {
        _usersContext = usersContext;
    }

    public Task<AuthenticationLogin?> GetByLogin(Login login)
    {
        return _usersContext.AuthenticationLogins
            .Include(l => l.Employee)
            .FirstOrDefaultAsync(l => l.Login == login);
    }
}