using Lms.Users.Domain.Sessions;
using Microsoft.EntityFrameworkCore;

namespace Lms.Users.Infrastructure.DataAccess.Sessions;

public class SessionRepository : ISessionRepository
{
    private readonly UsersContext _usersContext;

    public SessionRepository(UsersContext usersContext)
    {
        _usersContext = usersContext;
    }

    public Task<Session?> GetById(Guid id)
    {
        return _usersContext.Sessions
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public void Add(Session session)
    {
        _usersContext.Add(session);
    }

    public void Update(Session session)
    {
        _usersContext.Update(session);
    }
}